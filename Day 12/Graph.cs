using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_12
{
    class Graph
    {
        public static readonly double INFINITY = System.Double.MaxValue;

        public Dictionary<string, Vertex> vertexMap;

        public Graph()
        {
            vertexMap = new Dictionary<string, Vertex>();
        }

        public void AddVertex(string name)
        {
            if (vertexMap.FirstOrDefault(d => d.Key == name).Value == null) vertexMap.Add(name, new Vertex(name));
        }

        public Vertex GetVertex(string name)
        {
            Vertex v = vertexMap.FirstOrDefault(d => d.Key == name).Value;
            if (v == null)
            {
                AddVertex(name);
                v = vertexMap.FirstOrDefault(d => d.Key == name).Value;
            }
            return v;
        }

        public void AddEdge(string source, string dest, double cost = 1)
        {
            Vertex s = GetVertex(source);
            Vertex d = GetVertex(dest);
            s.adj.AddLast(new Edge(d, cost));
        }

        public void ClearAll()
        {
            foreach (Vertex v in vertexMap.Values)
            {
                v.Reset();
            }
        }

        public void Unweighted(string name)
        {
            ClearAll();

            Vertex start = GetVertex(name);
            if (start == null) throw new Exception("Start vertex not found");
            Queue<Vertex> q = new Queue<Vertex>();
            q.Enqueue(start);
            start.distance = 0;

            while (q.Count != 0)
            {
                Vertex from = q.Dequeue();
                foreach (Edge e in from.adj)
                {
                    Vertex dest = e.dest;
                    if (dest.distance == Graph.INFINITY)
                    {
                        dest.distance = from.distance + 1;
                        dest.prev = from;
                        q.Enqueue(dest);
                    }
                }
            }
        }
    }
}
