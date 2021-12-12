using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_12
{
    class Vertex
    {
        public string name;
        public LinkedList<Edge> adj;
        public double distance;
        public Vertex prev;
        public bool known;

        public Vertex(string name)
        {
            this.name = name;
            adj = new LinkedList<Edge>();
            distance = Graph.INFINITY;
        }

        public void Reset()
        {
            prev = null;
            distance = Graph.INFINITY;
            known = false;
        }
        public int CompareTo(Vertex other)
        {
            if (this.distance < other.distance) return -1;
            if (this.distance > other.distance) return 1;
            else return 0;
        }

        public override string ToString()
        {
            string s = $"{name} ";
            if (distance != Graph.INFINITY) s += $" ({distance}) ";
            s += "[";
            foreach (Edge e in adj.OrderBy(x => x.dest.name))
            {
                s += $"{e.dest.name} ({e.cost}) ";
            }
            s += "]";
            return s;
        }
    }
}
