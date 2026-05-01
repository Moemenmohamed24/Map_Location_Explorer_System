using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using GoogleMapsProject1;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;


public class GoogleAPIGet
{
    public string Id { get; set; }           
    public string Name { get; set; }     
    public string Description { get; set; }  
    public string Governorate { get; set; }     
    public bool IsNature { get; set; }
    public string AirQuality { get; set; }       
    public string Type { get; set; }
    public string OpeningHours { get; set; }
    public double Rating { get; set; }
    public double DistanceFromCenter { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}


public static class GooglePlaceSearcher
{

    private static readonly string ApiKey = "AIzaSyBmovKt7QOCidDCn3Zw8XQB14Macckr9To";
    private static readonly string origin = "30.0444,31.2357";
    private static readonly double lat1 = 30.0444;
    private static readonly double lon1 = 31.2357;
   
    public static async Task<List<GoogleAPIGet>> SearchPlacesAsyncAPI(string type, bool isWater, bool isNatural, string airQuality, int maxResults)
    {
        List<GoogleAPIGet> results = new List<GoogleAPIGet>();

        using (HttpClient http = new HttpClient())
        {
            http.Timeout = TimeSpan.FromMinutes(5);


            List<string> filters = new List<string> { $"[\"name\"~\"{type}\", i]" };
            if (isWater) filters.Add("[\"natural\"=\"water\"]");
            if (isNatural) filters.Add("[\"natural\"~\"wood|forest|park\"]");
            string overpassQuery;
            if (isWater)
            {
                overpassQuery = @"
[out:json][timeout:180];
(
  node[""natural""=""water""](30.0,31.0,31.0,32.0);
  way[""natural""=""water""](30.0,31.0,31.0,32.0);
  relation[""natural""=""water""](30.0,31.0,31.0,32.0);
);
out center;";
            }
            else if (isNatural)
            {
                overpassQuery = @"
[out:json][timeout:180];
(
  node[""natural""~""wood\|forest\|park""](30.0,31.0,31.0,32.0);
  way[""natural""~""wood\|forest\|park""](30.0,31.0,31.0,32.0);
  relation[""natural""~""wood\|forest\|park""](30.0,31.0,31.0,32.0);
);
out center;";
            }
            else
            {
                overpassQuery = $@"
[out:json][timeout:180];
(
  node[""name""~""{EscapeSimple(type)}"",i](30.0,31.0,31.0,32.0);
  way[""name""~""{EscapeSimple(type)}"",i](30.0,31.0,31.0,32.0);
  relation[""name""~""{EscapeSimple(type)}"",i](30.0,31.0,31.0,32.0);
);
out center;";
            }
            string url = "https://overpass.kumi.systems/api/interpreter?data=" + Uri.EscapeDataString(overpassQuery);


            Console.WriteLine(url);
            
            //convert json to object in c# 
            //get the json

            //get result from API
            var response = await http.GetAsync(url);
            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                int count = 0;

                // get the JSON response
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // convert JSON to object in C#
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    var elements = doc.RootElement.GetProperty("elements");

                    foreach (var element in elements.EnumerateArray())
                    {                        
                        double lat = 0, lng = 0;
                        if (element.TryGetProperty("lat", out var latProp))
                            lat = latProp.GetDouble();
                        if (element.TryGetProperty("lon", out var lonProp))
                            lng = lonProp.GetDouble();
                        else if (element.TryGetProperty("center", out var centerProp))
                        {
                            lat = centerProp.GetProperty("lat").GetDouble();
                            lng = centerProp.GetProperty("lon").GetDouble();
                        }

                        string name = element.TryGetProperty("tags", out var tags) && tags.TryGetProperty("name", out var nameProp)
                            ? nameProp.GetString()
                            : "Unnamed";

                        string destination = $"{lat},{lng}";
                        double distanceFromCenter = await calculateDistanceFromCapital.GetDistance(lat1, lon1, lat, lng);
                        string description = await GetDescribtionAsync.GetDescriptionAsync(name);
                        string governorate = await GetGovernorateAsync.GetGovernorate(lat, lng);

                        if (count >= maxResults) break;

                        results.Add(new GoogleAPIGet
                        {
                            Id = element.GetProperty("id").GetRawText(),
                            Name = name,
                            Description = description,
                            Governorate = governorate,
                            IsNature = isNatural,
                            AirQuality = airQuality,
                            Type = type,
                            OpeningHours = "NULL", // Overpass doesn’t provide this info
                            Rating = 0, // Overpass doesn’t have ratings
                            Latitude = lat,
                            Longitude = lng,
                            DistanceFromCenter = distanceFromCenter
                        });

                        count++;
                    }
                }
            }
            else
            {
                string errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error when fetching data from Overpass API: {response.StatusCode}\n{errorMsg}");
            }

        }
        return results;
    }

    private static object EscapeSimple(string type)
    {
        throw new NotImplementedException();
    }
};

public static class ConvertFormateTOString
{

    public static List<string> GetstringFormat(List<GoogleAPIGet> results)
    {
        List<string> finalresult = new List<string>();


        foreach(var str in results)
        {
            finalresult.Add(
            $"Id: {str.Id} , " +
            $"Name: {str.Name}, " +
            $"Description: {str.Description}, " +
            $"Governorate: {str.Governorate}, " +
            $"IsNature: {str.IsNature}, " +
            $"AirQuality: {str.AirQuality}, " +
            $"Type: {str.Type}, " +
            $"OpeningHours: {str.OpeningHours}, " +
            $"Rating: {str.Rating}, " +
            $"Latitude: {str.Latitude}, " +
            $"Longitude: {str.Longitude}, " +
            $"DistanceFromCenter: {str.DistanceFromCenter}"
            );  
        }
        return finalresult;
    }     

}


public static class GetGovernorateAsync
{
    public static async Task<string> GetGovernorate(double lat, double lng)
    {          
                      //result be json      get all the area        get the gove        //get name of the gove  //qt = quick tag
        string query = $@"[out:json];      is_in({lat},{lng})->.a; area.a[admin_level=6];out tags qt;";

        string url = "https://overpass-api.de/api/interpreter?data=" + Uri.EscapeDataString(query);

        // object conect with the outspurce
        using (HttpClient client = new HttpClient())
        {
            //get the json respons
            var respons = await client.GetAsync(url);

            if(!respons.IsSuccessStatusCode)
            {
                return "Unknown";
            }

            //get convert json to string
            string json = await respons.Content.ReadAsStringAsync();


            if (!json.TrimStart().StartsWith('{'))
            {
                throw new Exception("Invalid JSON format: " + json);
            }

            using (JsonDocument doce = JsonDocument.Parse(json))
            {

                var elements = doce.RootElement.GetProperty("elements");

                foreach (var element in elements.EnumerateArray())
                {
                    if(element.TryGetProperty("tags",out var tags))
                    {
                        if (tags.TryGetProperty("admin_level", out var level) && level.GetString() == "6")
                        {
                            if (tags.TryGetProperty("name:ar", out var nameAr))
                                return nameAr.GetString();

                            if (tags.TryGetProperty("name", out var name))
                                return name.GetString();
                        }
                    }

                }
            }
        }

            return "Unknown";
    }

}


public static class GetDescribtionAsync
{
    private static readonly string ApiKey = "AIzaSyBmovKt7QOCidDCn3Zw8XQB14Macckr9To";
    public static async Task<string> GetDescriptionAsync(string placeName)
    {

          if(string.IsNullOrWhiteSpace(placeName))
          {
            return "the placename is empty...";
          }

        string url = "https://ar.wikipedia.org/api/rest_v1/page/summary/" + Uri.EscapeDataString(placeName);


        using (HttpClient http = new HttpClient())
        {
            var response = await http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return "no description";

            string json = await response.Content.ReadAsStringAsync();

            if(!json.TrimStart().StartsWith('{'))
            {              
                    throw new Exception("Invalid JSON format: " + json);                
            }

            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                if (doc.RootElement.TryGetProperty("extract", out var extract))
                    return extract.GetString();
            }
        }
        return "no description";
    }
}


public static class calculateDistanceFromCapital
{

        // دالة تحسب المسافة بين نقطتين بناءً على خطوط الطول والعرض
        public static async Task<double>GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371.0; // نصف قطر الأرض بالكيلومتر

            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // المسافة بالكيلومتر
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }   

}

namespace GoogleMapsProjects1
{

    class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());                     

            /*
            Graph graph = new Graph();

            graph = GraphLoader.LoeadEdgsFromDatabase();

            string start = "Cairo";
            string end = "Aswan";

            //Tuple Deconstruction
            var (distance,prevuse) =  Dijkstra.ComputeShortestPaths(graph, start);

            if(distance.ContainsKey(end))
            {
                Console.WriteLine($"the shortesh path from {start} to {end} = {distance[end]} km");

                 List<string> path = Dijkstra.ReconstructPath(prevuse,end,start);

                Console.WriteLine($"the path from {start} to {end} is   {string.Join("->", path)}");
            }
            else
            {
                Console.WriteLine($"No path found from {start} to {end}..");
            }
            */           

        }
    }
}

