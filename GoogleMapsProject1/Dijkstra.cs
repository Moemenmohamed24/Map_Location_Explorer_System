namespace GoogleMapsProjects1
{
    public static class Dijkstra
    {


        public static (Dictionary<string, double> distance, Dictionary<string, string> previous) ComputeShortestPaths(Graph graph, string start)
        {



            var distance = new Dictionary<string, double>();
            var previous = new Dictionary<string, string>();
            var visited = new HashSet<string>();
            var pirority = new PriorityQueue<string, double>();





            foreach (var node in graph.GetadjacencyList().Keys)
            {

                distance[node] = double.PositiveInfinity;
                previous[node] = null;

            }


            distance[start] = 0;
            pirority.Enqueue(start, 0);

            //this is handle the  neighbors in the one node and get the piroity
            while (pirority.Count > 0)
            {

                var currentNode = pirority.Dequeue();

                if (visited.Contains(currentNode))
                    continue;


                visited.Add(currentNode);


                if (!graph.GetadjacencyList().ContainsKey(currentNode))
                {
                    continue;
                }



                foreach (var (neighbor, distantofneighbor) in graph.GetadjacencyList()[currentNode])
                {

                    double newdistanse = distance[currentNode] + distantofneighbor;

                    if (newdistanse < distance.GetValueOrDefault(neighbor, double.PositiveInfinity))//comber the newdistanc in the new path & the dist in the neighbor
                    {

                        distance[neighbor] = newdistanse;
                        previous[neighbor] = currentNode;//like this
                                                         //"Aswan": "Luxor",
                                                         //"Luxor": "Qena",
                                                         //"Qena": "Sohag",
                                                         //"Sohag": "Cairo";

                        pirority.Enqueue(neighbor, newdistanse);
                    }

                }


            }

            return (distance, previous);

        }

        public static List<string> ReconstructPath(Dictionary<string, string> preivus, string end, string start)
        {

            string current = end;

            List<string> path = new List<string>();
            while (current != null)
            {
                path.Insert(0, current);
                current = preivus[current];
            }

            if (path.Count == 0 || path[0] != start)
            {
                return new List<string>();
            }

            return path;
        }
    };
}
