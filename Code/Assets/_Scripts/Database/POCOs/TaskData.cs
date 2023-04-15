using System;
using Newtonsoft.Json;

public class TaskData : Data
{
    [JsonProperty("id")] public string ID { get; set; }
    [JsonProperty("employee_id")] public string EmployeeID { get; set; }
    [JsonProperty("mcp_id")] public string MCPID { get; set; }

    [JsonProperty("vehicle_id")] public string VehicleID { get; set; }
    [JsonProperty("timeToDo")] public string Timestamp { get; set; }
    [JsonProperty("checkin")] public int CheckedIn { get; set; }
    [JsonProperty("checkout")] public int CheckedOut { get; set; }
}