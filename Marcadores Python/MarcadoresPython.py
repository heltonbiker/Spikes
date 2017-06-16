#!/usr/bin/env python
# coding: utf-8

import gtk
import cairo
import Image
import array
import json
from math import *


def distance(p1,p2):
    x1, y1 = p1
    x2, y2 = p2
    return sqrt((x2-x1)**2 + (y2-y1)**2)

def mousemove(widget, event):
    widget.mouse_position = (event.x, event.y)
    widget.queue_draw()

def mousepress(widget, event):
    if event.button == 1:
        widget.point_clicked = (event.x, event.y)
    elif event.button == 3:
        if widget.markers:
            for marker in widget.markers:
                if distance(marker, (event.x, event.y)) < widget.ring_radius:
                    widget.markers.remove(marker)

    widget.clicked_button = event.button
    widget.queue_draw()

def mouserelease(widget, event, button):
    if widget.point_clicked:
        x, y = widget.point_clicked
        releasex = (x - ((x - event.x)*widget.scale)/ widget.markerzoom)
        releasey = (y - ((y - event.y)*widget.scale)/ widget.markerzoom)
        widget.markers.append((releasex, releasey))
    widget.clicked_button = None
    widget.point_clicked = None
    if len(widget.markers) == 4:
        button.set_sensitive(True)
        button.grab_focus()
    else:
        button.set_sensitive(False)
        widget.coded_markers = {}
    widget.queue_draw()


class ClickableImage(gtk.DrawingArea):
    def __init__(self, imagename, buttonname):
        gtk.DrawingArea.__init__(self)
        self.connect("expose_event", self.expose)
        self.connect("motion_notify_event", mousemove)
        self.connect("button_press_event", mousepress)
        self.connect("button_release_event", mouserelease, buttonname)
        self.add_events(gtk.gdk.EXPOSURE_MASK
                    | gtk.gdk.LEAVE_NOTIFY_MASK
                    | gtk.gdk.BUTTON_PRESS_MASK
                    | gtk.gdk.BUTTON_RELEASE_MASK
                    | gtk.gdk.POINTER_MOTION_MASK
                    | gtk.gdk.POINTER_MOTION_HINT_MASK
                    | gtk.gdk.SCROLL)


        self.im = Image.open(imagename)
        self.width, self.height = self.im.size
        self.nominal_height = 700.0
        self.scale = self.nominal_height/self.height
        self.nominal_width = self.width*self.scale
        self.set_size_request(int(self.nominal_width), int(self.nominal_height))

        a = array.array('B', self.im.tostring('raw', 'BGRA'))
        self.photo = cairo.ImageSurface.create_for_data(a,
                                                       cairo.FORMAT_RGB24,
                                                       self.width,
                                                       self.height,
                                                       self.width*4)

        self.mouse_position = None
        self.point_clicked = None
        self.clicked_button = None

        self.markers = []
        self.coded_markers = {}
        self.markerzoom = 10
        self.ring_radius = 20



    def expose(self, widget, event):
        cr = widget.window.cairo_create()
        rect = self.get_allocation()
        w = rect.width
        h = rect.height

        cr.save()
        cr.scale(self.scale, self.scale)
        cr.set_source_surface(self.photo, 0, 0)
        cr.paint()
        cr.restore()

        if self.markers:
            cr.set_source_rgb(0,0,1)
            for marker in self.markers:
                x, y = marker
                cr.arc(x, y, 3, 0, 2*pi)
                cr.fill()

        if self.coded_markers:
            cr.save()
            cr.set_source_rgb(1,1,1)
            cr.set_font_size(20)
            for k, p in self.coded_markers.items():
                cr.move_to(*p)
                cr.rel_move_to(10,0)
                cr.show_text(k)
            cr.restore()


        if self.mouse_position:
            cr.new_path()
            cr.save()
            x, y = self.mouse_position
            alpha = 0.0
            radius = self.ring_radius
            cross = False
            if self.point_clicked:
                x, y = self.point_clicked
                alpha = 1.0
                radius *= self.markerzoom
                cross = True

            cr.set_source_rgb(0,0.6,0)
            cr.arc(x,y, radius, 0, 2*pi)
            cr.stroke_preserve()
            cr.clip()

            cr.translate(x, y)
            cr.scale(self.markerzoom, self.markerzoom)
            cr.set_source_surface(self.photo, -x/self.scale, -y/self.scale)
            cr.paint_with_alpha(alpha)
            cr.restore()

            if cross:
                cr.set_source_rgb(0,0,0)
                cr.set_line_width(1)
                cr.move_to(*self.mouse_position)
                l = 4*self.markerzoom
                cr.rel_move_to(-l,0)
                cr.rel_line_to(2*l,0)
                cr.rel_move_to(-l,-l)
                cr.rel_line_to(0,2*l)
                cr.stroke()

def finish_handler(widget, clickable, outputname):

    assert(len(clickable.markers) == 4)

    yorder = sorted(clickable.markers, key=lambda x:x[1])
    clickable.coded_markers['vp'] = yorder[0]

    xbottomorder = sorted(yorder[1:])
    assert(len(xbottomorder) == 3)

    clickable.coded_markers['dl'] = xbottomorder[0]
    clickable.coded_markers['sp'] = xbottomorder[1]
    clickable.coded_markers['dr'] = xbottomorder[2]


    ### OS MARCADORES ESTÃO SENDO SALVOS EM LISTA, ACHO QUE SERIA MAIS ÚTIL SALVÁ-LOS EM DICIONÁRIO (vp, sp, dl, dr)
    with open(outputname, 'w') as outfile:
        finalmarkers = []
        for n in xrange(len(clickable.markers)):
            finalmarkers.append((clickable.markers[n][0]/clickable.scale,
                                    clickable.markers[n][1]/clickable.scale))

        json.dump(finalmarkers, outfile)
        print "{} saved!".format(outputname)
    #clickable.queue_draw()
    widget.get_toplevel().destroy()
    gtk.main_quit()


def MarkReferencePoints(inputname):

    print "Executing Markers Location (GUI)"

    window = gtk.Window()
    window.connect("destroy", gtk.main_quit)
    panel = gtk.HBox()
    window.add(panel)

    button = gtk.Button('Concluir')
    button.set_sensitive(False)

    clickable = ClickableImage(inputname, button)
    #button.connect('clicked', finish_handler, clickable)
    panel.pack_end(clickable, False, False)

    sidepanel = gtk.VBox()
    sidepanel.set_size_request(300,-1)
    panel.pack_start(sidepanel, False, False)



    sidepanel.pack_end(button, False, False)

    window.show_all()
    window.set_position(gtk.WIN_POS_CENTER)
    gtk.main()


if __name__ == '__main__':

    MarkReferencePoints('white.png')




