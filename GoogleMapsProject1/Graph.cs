namespace GoogleMapsProjects1
{
    public class Graph
    {

        private Dictionary<string, List<(string to, double Distance)>> adjacencyList = new();
        //List<(string to, double Distance) this is tuble


        private void Add_Node(string place)
        {

            if (!adjacencyList.ContainsKey(place))
            {
                adjacencyList[place] = new List<(string to, double Distance)>();
            }
            // adjacencyList[place] the key of node
            //new List<(string to, double Distance)>(); the value of the key 

        }
        public void Add_Edge(string from, string to, double Distance)
        {

            Add_Node(from);
            adjacencyList[from].Add((to, Distance));
        }


        public Dictionary<string, List<(string to, double Distance)>> GetadjacencyList()
        {
            return adjacencyList;
        }
        // abliy the encabsulation
    };
}
