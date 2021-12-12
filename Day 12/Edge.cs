using System;
using System.Collections.Generic;
using System.Text;

namespace Day_12
{
    class Edge
    {
        public Vertex dest;
        public double cost;

        public Edge(Vertex d, double c)
        {
            dest = d;
            cost = c;
        }
    }
}
