#coding:utf-8

import numpy
import matplotlib.pyplot as plt
from collections import namedtuple
from pprint import pprint as pp
import os

inf = float('inf')
Edge = namedtuple('Edge', 'start, end, cost')

class Graph():



    def __init__(self, edges):
        self.arestas = edges2 = [Edge(*edge) for edge in edges]
        self.vertices_set = set(sum(([e.start, e.end] for e in edges2), []))  # apenas um set com os vértices

    def dijkstra(self, source, dest):
        assert source in self.vertices_set

        # cria um dicionário de valores acumulados (inicialmente infinito)
        dist = {vertex: inf for vertex in self.vertices_set}

        # cria um dicionário com os vértices visitados (inicialmente não visitados)
        previous = {vertex: None for vertex in self.vertices_set}

        # zera o valor acumulado no ponto de origem
        dist[source] = 0

        queue = self.vertices_set.copy()

        neighbours = {vertex: set() for vertex in self.vertices_set}

        for start, end, cost in self.arestas:
            neighbours[start].add((end, cost))

        while queue:
            closest = min(queue, key=lambda v:dist[v])
            queue.remove(closest)
            if dist[closest] == inf or closest == dest:
                break
            for end, cost in neighbours[closest]:
                alt = dist[closest] + cost
                if alt < dist[end]:
                    dist[end] = alt
                    previous[end] = closest
        s, closest = [], dest
        while previous[closest]:
            s.insert(0, closest)
            closest = previous[closest]
        s.insert(0, closest)
        return s



def atribuir_peso(up, lp):

    upkey, upval = up
    lokey, loval = lp

    peso = loval[2]

    dy = abs(upval[0] - loval[0])
    dx = abs(upval[1] - loval[1])

    distancia = (dx**2 + dy**2)**0.5

    angulo = numpy.arctan2(dx, dy)





    return distancia + peso * 10




basedir = "C:/Miotec/Vert3d/Exames/"


for apasta in sorted(os.listdir(basedir), reverse=True)[:20]:


    pasta = basedir + apasta + '/'



    m = numpy.genfromtxt(pasta + 'minimos_assimetria.csv', delimiter=",", missing_values="")


    plt.imshow(m, origin='lower', interpolation='nearest', cmap='pink')

    for h in xrange(m.shape[0]):
        plt.axhline(h, color='cyan', alpha=0.2)


    marcadores = numpy.genfromtxt(pasta + 'marcadores.csv', delimiter=",", missing_values="")
    linhamarcadores = marcadores[:,-2:]
    plt.plot(linhamarcadores[:,1], linhamarcadores[:,0], 'go', mew=0)


    nodes = []
    singlenodes = []
    nodecount = 0
    for i, row in enumerate(m):
        noderow = []
        for j, val in enumerate(row):
            if not numpy.isnan(val):
                noderow.append([nodecount,[i,j,val]])
                singlenodes.append([nodecount,[i,j,val]])
                nodecount += 1
                #plt.scatter(j, i, s=12, linewidths=0, color='green', alpha=0.8)
        if noderow:
            nodes.append(noderow)


    edges = []
    for n in xrange(1, len(nodes)):
        upperline = nodes[n-1]
        lowerline = nodes[n]
        for up in upperline:
            for lp in lowerline:

                peso = atribuir_peso(up, lp)

                edges.append((up[0], lp[0], peso))

                # adiciona um edge (key, key, val) à lista, baseado em up e down

                #plt.plot(segment[:,1], segment[:,0], lw=0.5, color='k', alpha=0.5)

    graph = Graph(edges)


    ## Escolhe os melhores pontos iniciais:
    startmarker = linhamarcadores[0,:]
    endmarker = linhamarcadores[1,:]
    startmindist = numpy.inf
    endmindist = numpy.inf
    startindex = 0;
    endindex = len(singlenodes)-1
    for node in singlenodes:

        indice, val = node
        i, j, peso = val

        mi, mj = startmarker
        dist = ((mi-i)**2 + (mj-j)**2)**0.5
        if dist < startmindist:
            startmindist = dist
            startindex = indice

        mi, mj = endmarker
        dist = ((mi-i)**2 + (mj-j)**2)**0.5
        if dist < endmindist:
            endmindist = dist
            endindex = indice




    points = []
    for k in graph.dijkstra(startindex, endindex):
        points.append(singlenodes[k][1])

    shortest = numpy.array(points)

    print shortest; exit()

    plt.plot(shortest[:,1], shortest[:,0], 'r')

    plt.axis('equal')
    plt.gca().invert_yaxis()
    plt.show()

