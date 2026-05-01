using System.Data.SqlClient;

namespace GoogleMapsProjects1
{
    public static class GraphLoader
    {

        public static Graph LoeadEdgsFromDatabase()
        {
            Graph graph = new Graph();


            string connectionString = "Data Source=DESKTOP-JVK01UB;Initial Catalog=DBmaps;Integrated Security=True;";


            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();


            string query = "select [FromGovernorate], [ToGovernorate] ,[DistanceKm]  from [DirectGovernorateLinks]";

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                string from = reader.GetString(0);
                string to = reader.GetString(1);
                double distance = reader.GetDouble(2);

                graph.Add_Edge(from, to, distance);
            }
            return graph;

        }


    };
}
