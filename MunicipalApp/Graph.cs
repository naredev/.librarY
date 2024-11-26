using System;
using System.Collections.Generic;

namespace MunicipalApp
{
    public class Graph<T>
    {
        // Dictionary to store the adjacency list for each node
        private Dictionary<T, List<T>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<T, List<T>>();
        }

        // Adds a node to the graph
        public void AddNode(T node)
        {
            if (!adjacencyList.ContainsKey(node))
            {
                adjacencyList[node] = new List<T>();
            }
        }

        // Adds a directed edge from one node to another
        public void AddEdge(T from, T to)
        {
            if (!adjacencyList.ContainsKey(from))
            {
                AddNode(from);
            }
            if (!adjacencyList.ContainsKey(to))
            {
                AddNode(to);
            }
            adjacencyList[from].Add(to);
        }

        //Part of this code was adapted from GeeksforGeeks
        //https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/
        //Accessed on the 30 October 2024
        // Method to perform a Breadth-First Traversal from a given node
        public List<T> BreadthFirstTraversal(T startNode)
        {
            var visited = new HashSet<T>();
            var queue = new Queue<T>();
            var result = new List<T>();

            queue.Enqueue(startNode);
            visited.Add(startNode);

            while (queue.Count > 0)
            {
                T currentNode = queue.Dequeue();
                result.Add(currentNode);

                foreach (var neighbor in adjacencyList[currentNode])
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
            return result;
        }

        // Method to check if there's a path from one node to another
        public bool HasPath(T from, T to)
        {
            if (!adjacencyList.ContainsKey(from) || !adjacencyList.ContainsKey(to))
                return false;

            var visited = new HashSet<T>();
            return DepthFirstSearch(from, to, visited);
        }

        // Depth-First Search helper for pathfinding
        private bool DepthFirstSearch(T current, T target, HashSet<T> visited)
        {
            if (current.Equals(target))
                return true;

            visited.Add(current);

            foreach (var neighbor in adjacencyList[current])
            {
                if (!visited.Contains(neighbor) && DepthFirstSearch(neighbor, target, visited))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
