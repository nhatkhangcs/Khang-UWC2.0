using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class VehicleDatabaseCommunicator : MonoBehaviour
{
    private const string GET_VEHICLE_INFO_PATH = "/api/vehicle/info/{0}";
    private const string GET_ALL_VEHICLE_INFO_PATH = "/api/vehicle/all";

    public void GetAllVehicle(Action<bool, List<VehicleData>> callback)
    {
        StartCoroutine(GetAllVehicle_CO(callback));
    }

    private IEnumerator GetAllVehicle_CO(Action<bool, List<VehicleData>> callback)
    {
        var request = BackendCommunicator.CreateGetRequest(GET_ALL_VEHICLE_INFO_PATH);
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.ProtocolError)
        {
            callback?.Invoke(false, null);
            yield break;
        }

        var mcpData = JsonConvert.DeserializeObject<List<VehicleData>>(request.downloadHandler.text);

        callback?.Invoke(true, mcpData);
    }

    private IEnumerator GetVehicleInfo_CO(string vehicleId)
    {
        var request =
            BackendCommunicator.CreateGetRequest(string.Format(GET_VEHICLE_INFO_PATH, vehicleId));
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) Debug.LogWarning("Request failed.");
        Debug.Log(request.downloadHandler.text);
    }
}