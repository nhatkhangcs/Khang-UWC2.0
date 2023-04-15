using System;
using Newtonsoft.Json;
using UnityEngine;

public class StaffData : Data
{
    [JsonProperty("id")] public string ID { get; set; }
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("image")] public object Image { get; set; }

    [JsonProperty("gender")] public string Gender { get; set; }

    [JsonProperty("dob")] public DateTime DateOfBirth { get; set; }

    [JsonProperty("address")] public string HomeAddress { get; set; }

    [JsonProperty("phone")] public string PhoneNumber { get; set; }

    [JsonProperty("nationality")] public string Nationality { get; set; }

    [JsonProperty("hired_on")] public DateTime HiredOn { get; set; }

    [JsonProperty("role")] public string Role { get; set; }

    [JsonProperty("salary")] public int Salary { get; set; }

    [JsonProperty("username")] public string Username { get; set; }
}