using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericDijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<Edge> Results = Engine.CalculateShortestPathBetween(
            1,
            5,
            new Edge[] {
                new Edge() { Origem = 1, Destino = 2, Cost = 3 },
                new Edge() { Origem = 1, Destino = 3, Cost = 3 },
                new Edge() { Origem = 1, Destino = 4, Cost = 5 },
 
                new Edge() { Origem = 2, Destino = 3, Cost = 3 },
 
                new Edge() { Origem = 3, Destino = 4, Cost = 3 },
 
                new Edge() { Origem = 4, Destino = 5, Cost = 3 }
            });
        }
    }

    //public class Node
    //{
    //    public int Label { get; set; }
    //    public int I { get; set; }
    //    public int J { get; set; }
    //    public double A { get; set; }

    //    public Node (int label, int i, int j, double a)
    //    {
    //        Label = label;
    //        I = i;
    //        J = j;
    //        A = a;
    //    }
    //}


    public class Edge
    {
        public int Origem { get; set; }
        public int Destino { get; set; }

        // REFACTOR: passar isso pra get-only e pra double;
        public int Cost { get; set; }
    }


    public static class Engine
    {
        public static LinkedList<Edge> CalculateShortestPathBetween(int source, int destination, IEnumerable<Edge> edges)
        {
            LinkedList<Edge> result = CalculateFrom(source, edges)[destination];
            return result;
        }

        private static Dictionary<int, LinkedList<Edge>> CalculateFrom(int origem, IEnumerable<Edge> edges)
        {
            if (edges.Any(edge => edge.Origem.Equals(edge.Destino)))
                throw new ArgumentException("No path can have the same source and destination");

            var ShortestPathsSoFar = new Dictionary<int, KeyValuePair<int, LinkedList<Edge>>>();           
            var LocationsAlreadyProcessed = new List<int>();

            // include all possible steps, with Int.MaxValue cost
            edges.SelectMany(edge => new int[] { edge.Origem, edge.Destino })                 // union source and destinations
                 .Distinct()                                                         // remove duplicates
                 .ToList()                                                           // ToList exposes ForEach
                 .ForEach(s => ShortestPathsSoFar.Set<int>(s, int.MaxValue, null));  // add to ShortestPaths with MaxValue cost

            // update cost for self-to-self as 0; no path
            ShortestPathsSoFar.Set(origem, 0, null);

            // keep this cached
            var LocationCount = ShortestPathsSoFar.Keys.Count;

            while (LocationsAlreadyProcessed.Count < LocationCount)
            {
                int _locationToProcess = default(int);

                //Search for the nearest location that isn't handled
                foreach (int _location in ShortestPathsSoFar.OrderBy(p => p.Value.Key).Select(p => p.Key).ToList())
                {
                    if (!LocationsAlreadyProcessed.Contains(_location))
                    {
                        if (ShortestPathsSoFar[_location].Key == Int32.MaxValue)
                            return ShortestPathsSoFar.ToDictionary(k => k.Key, v => v.Value.Value); //ShortestPaths[destination].Value;

                        _locationToProcess = _location;
                        break;
                    }
                } // foreach

                var _selectedEdges = edges.Where(p => p.Origem.Equals(_locationToProcess));
                foreach (Edge edge in _selectedEdges)
                {
                    if (ShortestPathsSoFar[edge.Destino].Key > edge.Cost + ShortestPathsSoFar[edge.Origem].Key)
                    {
                        ShortestPathsSoFar.Set(
                            edge.Destino,
                            edge.Cost + ShortestPathsSoFar[edge.Origem].Key,
                            ShortestPathsSoFar[edge.Origem].Value.Union(new Edge[] { edge }).ToArray());
                    }
                } // foreach

                //Add the location to the list of processed locations
                LocationsAlreadyProcessed.Add(_locationToProcess);

            } // while

            return ShortestPathsSoFar.ToDictionary(k => k.Key, v => v.Value.Value);
            //return ShortestPaths[destination].Value;
        }
    }


    public static class ExtensionMethods
    {
        /// <summary>
        /// Adds or Updates the dictionary to include the destination and its associated cost and complete path (and param arrays make paths easier to work with)
        /// </summary>
        public static void Set<T> (this Dictionary<T, KeyValuePair<int, LinkedList<Edge>>> Dictionary,
                                   T destination,
                                   int Cost,
                                   params Edge[] paths)
        {
            LinkedList<Edge> CompletePath = paths == null ? new LinkedList<Edge>()
                                                          : new LinkedList<Edge>(paths);
            Dictionary[destination] = new KeyValuePair<int, LinkedList<Edge>>(Cost, CompletePath);
        }
    }

}
