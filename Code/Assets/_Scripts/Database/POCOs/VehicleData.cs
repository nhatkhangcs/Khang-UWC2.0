using Mapbox.Json;

public class VehicleData : Data
{
    [JsonProperty("id")] public string ID { get; set; }
    [JsonProperty("category")] public string Category { get; set; }

    [JsonProperty("model")] public string Model { get; set; }

    [JsonProperty("weight")] public int Weight { get; set; }

    [JsonProperty("capacity")] public int Capacity { get; set; }

    [JsonProperty("fuel_consumption")] public int FuelConsumption { get; set; }
}