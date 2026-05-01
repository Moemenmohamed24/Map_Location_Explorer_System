using System.Data.SqlClient;
using System.Data;

namespace GoogleMapsProjects1
{
    public static class PlaceSearcher
    {

        private static string SafeGet(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? reader[columnName].ToString() : "NULL";
        }
        public static List<string> SearchForplace(string typeofplace, bool is_water, bool is_natural, string Air_quality, int numberofPlace)
        {

            List<string> result = new List<string>();

            string connectionString = "Data Source=DESKTOP-JVK01UB;Initial Catalog=DBmaps;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();


                using (SqlCommand commnd = new SqlCommand("GetPlacesByTypeAndAttributes", connection))//connection her becuse we want the way of connection or the path 
                {

                    //add value to the StoredProcedure id Db
                    commnd.CommandType = CommandType.StoredProcedure;

                    commnd.Parameters.AddWithValue("@is_water", is_water);
                    commnd.Parameters.AddWithValue("@is_nature", is_natural);
                    commnd.Parameters.AddWithValue("@air_quality", Air_quality);
                    commnd.Parameters.AddWithValue("@maxPerType", numberofPlace);
                    commnd.Parameters.AddWithValue("@Type", typeofplace);




                    using (SqlDataReader reader = commnd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            string Line =
                             $"ID: {SafeGet(reader, "id")}, " +
                             $"Name: {SafeGet(reader, "placeName")}, " +
                             $"Description: {SafeGet(reader, "description")}, " +
                             $"Governorate: {SafeGet(reader, "governoratesName")}, " +
                             $"Nature: {SafeGet(reader, "is_nature")}, " +
                             $"Air Quality: {SafeGet(reader, "air_quality")}, " +
                             $"Type: {SafeGet(reader, "type")}, " +
                             $"Opening Hours: {SafeGet(reader, "opening_houre")}, " +
                             $"Rating: {SafeGet(reader, "rating")}, " +
                             $"Distance: {SafeGet(reader, "distance_from_center")}";

                            result.Add(Line);

                        }
                    }
                }

            }
            return result;
        }

    };
}
