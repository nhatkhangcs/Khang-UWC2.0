using System.Collections.Generic;
using Newtonsoft.Json;

public class Coordinate
{
    [JsonProperty("latitude")] public double Latitude { get; set; }
    [JsonProperty("longitude")] public double Longitude { get; set; }

    public Coordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}

public class CollectorCurrentPositionData
{
    [JsonProperty("collectorId")] public string CollectorId { get; set; }
    [JsonProperty("point")] public Coordinate Coordinate { get; set; }
}

public class CollectorRouteData
{
    [JsonProperty("collectorId")] public string CollectorId { get; set; }
    [JsonProperty("points")] public List<Coordinate> Route { get; set; }
}

public class CollectorRouteTraversedData
{
    [JsonProperty("currentPos")] public Coordinate CurrentPos { get; set; }
    [JsonProperty("route")] public List<Coordinate> Route { get; set; }
}