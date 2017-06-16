#!/usr/bin/env python
# coding: utf-8


import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import numpy

f = []


vsegs = 10
usegs = vsegs * 2

raio = 10

for u in numpy.linspace(-numpy.pi, numpy.pi, usegs):
	for v in numpy.linspace(-numpy.pi * 0.5, numpy.pi * 0.5, vsegs):
		x = raio * numpy.cos(v) * numpy.sin(u)
		y = raio * numpy.cos(v) * numpy.cos(u)
		z = raio * numpy.sin(v)
		f.append((x,y,z))


f = numpy.array(f)





fig = plt.figure()
ax = Axes3D(fig)

ax.plot(f[:,0],
		f[:,1],
		f[:,2], 'o', ms=3, mew=0, color='#49AACF')


ax.view_init(elev=70, azim=-70)

ext = [-raio, raio]

ax.auto_scale_xyz(ext, ext, ext)
plt.show()



"""
                int segs = 10;
                double step = ( 2 * Math.PI ) / (segs + 1);

                Point3D[,] nodes = new Point3D[segs+1, segs+1];

                for (int u = 0; u <= segs; u++) {
                    for (int v = 0; v <= segs; v++) {
                        double U = u * step; // - Math.PI * 0.5;
                        double V = v * step;
                        double x = raio * Math.Cos(v) * Math.Sin(u);
                        double y = raio * Math.Cos(v) * Math.Cos(u);
                        double z = raio * Math.Sin(v);

                        nodes[u,v] = new Point3D(x,y,z);
                    }
                }

"""
