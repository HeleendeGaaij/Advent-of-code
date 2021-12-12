using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day_12
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadAllLines("Day12.txt").ToList();

            Graph graph = buildGraph(input);

            Console.WriteLine(PassagePathingPartOne(graph));
            Console.WriteLine(PassagePathingPartTwo(graph));
        }

        //get count of all different paths start to end
        static long PassagePathingPartOne(Graph graph)
        {
            Vertex start = graph.GetVertex("start");
            Queue<Vertex> path = new Queue<Vertex>();

            path.Enqueue(start);

            return findAllPathsRecursive(path, new List<Queue<Vertex>>(), false);
        }

        //get count of all different paths start to end, 1 small cave can be visited twice
        static long PassagePathingPartTwo(Graph graph)
        {
            Vertex start = graph.GetVertex("start");
            Queue<Vertex> path = new Queue<Vertex>();

            path.Enqueue(start);

            return findAllPathsRecursive(path, new List<Queue<Vertex>>(), true);
        }


        static long findAllPathsRecursive(Queue<Vertex> que, List<Queue<Vertex>> allKnownPaths, bool isPartTwo)
        {
            Vertex last = que.Last();
            //for every connection from last vertex
            foreach(Edge edge in last.adj)
            {
                Vertex next = edge.dest;
                Queue<Vertex> newQue = new Queue<Vertex>(que);

                //check if cave can be visited
                if (char.IsUpper(next.name[0]) || (!char.IsUpper(next.name[0]) && (!newQue.Contains(next) || (isPartTwo && smallCaveSecondVisit(newQue)))))
                {
                    //visit cave
                    newQue.Enqueue(next);
                    //path finished?
                    if (next.name == "end")
                    {
                        //remember finished path
                        allKnownPaths.Add(newQue);
                    }
                    else
                    {
                        //recursively find next vertex
                        findAllPathsRecursive(newQue, allKnownPaths, isPartTwo);
                    }
                }
            }
            //count all finished paths
            return allKnownPaths.Count;
        }

        //see if a second visit is still allowed
        static bool smallCaveSecondVisit(Queue<Vertex> que)
        {
            Queue<Vertex> controleQue = new Queue<Vertex>(que);

            while (controleQue.Count > 2)
            {
                Vertex v = controleQue.Dequeue();
                if (!char.IsUpper(v.name[0]) && controleQue.Contains(v)) return false; // vertex with lowercase appears twice already
            }
            //next vertex can be second small cave
            return true;
        }

        //make graph from input connections
        static Graph buildGraph(List<string> input)
        {
            Graph graph = new Graph();

            foreach (string line in input)
            {
                string[] nodes = line.Split('-');
                graph.AddEdge(nodes[0], nodes[1], 1);
                if (!(nodes[0] == "start") && !(nodes[1] == "end"))
                {
                    graph.AddEdge(nodes[1], nodes[0]);
                }
            }

            return graph;
        }
    }
}
