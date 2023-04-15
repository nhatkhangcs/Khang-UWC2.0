using System.Collections.Generic;


namespace MapboxDirectionRequestResult
{
    public class Admin
    {
        public string iso_3166_1 { get; set; }
    }

    public class Geometry
    {
        public List<List<double>> coordinates { get; set; }
        public string type { get; set; }
    }

    public class Leg
    {
        public List<object> via_waypoints { get; set; }
        public List<Admin> admins { get; set; }
        public double weight { get; set; }
        public double duration { get; set; }
        public List<object> steps { get; set; }
        public double distance { get; set; }
        public string summary { get; set; }
    }

    public class Result
    {
        public List<Route> routes { get; set; }
        public List<Waypoint> waypoints { get; set; }
        public string code { get; set; }
        public string uuid { get; set; }
    }

    public class Route
    {
        public bool country_crossed { get; set; }
        public string weight_name { get; set; }
        public double weight { get; set; }
        public double duration { get; set; }
        public double distance { get; set; }
        public List<Leg> legs { get; set; }
        public Geometry geometry { get; set; }
    }

    public class Waypoint
    {
        public double distance { get; set; }
        public string name { get; set; }
        public List<double> location { get; set; }
    }
}