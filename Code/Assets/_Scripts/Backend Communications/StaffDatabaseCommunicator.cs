using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;


public class StaffDatabaseCommunicator : MonoBehaviour
{
    private const string GET_EMPLOYEE_BY_USERNAME_PATH = "/api/employee/info/{0}";
    private const string GET_EMPLOYEE_INFO_PATH = "/api/employee/info/{0}";
    private const string GET_ALL_EMPLOYEE_INFO_BY_ROLE_PATH = "/api/employee/role";

    public void GetEmployeeByUsername(string username, Action<bool, StaffData> callback)
    {
        StartCoroutine(GetEmployeeByUsername_CO(username, callback));
    }

    private IEnumerator GetEmployeeByUsername_CO(string username, Action<bool, StaffData> callback)
    {
        var request =
            BackendCommunicator.CreateGetRequest(string.Format(GET_EMPLOYEE_BY_USERNAME_PATH, username));
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result is not (UnityWebRequest.Result.Success or UnityWebRequest.Result.ProtocolError
            ))
        {
            callback?.Invoke(false, null);
            yield break;
        }

        var staffData = JsonConvert.DeserializeObject<StaffData>(request.downloadHandler.text);
        callback?.Invoke(true, staffData);
    }

    public void GetAllStaff(Action<bool, List<StaffData>> callback)
    {
        StartCoroutine(GetAllStaffByRole_CO("Janitor", (janitorSuccess, janitorList) =>
        {
            if (!janitorSuccess) callback?.Invoke(false, null);
            else
            {
                StartCoroutine(GetAllStaffByRole_CO("Collector", (collectorSuccess, collectorList) =>
                {
                    if (!collectorSuccess) callback?.Invoke(false, null);
                    else
                    {
                        var list = new List<StaffData>();
                        list.AddRange(janitorList);
                        list.AddRange(collectorList);
                        callback?.Invoke(true, list);
                    }
                }));
            }
        }));
    }

    public void GetAllJanitor(Action<bool, List<StaffData>> callback)
    {
        StartCoroutine(GetAllStaffByRole_CO("Janitor", callback));
    }

    public void GetAllCollector(Action<bool, List<StaffData>> callback)
    {
        StartCoroutine(GetAllStaffByRole_CO("Collector", callback));
    }

    private IEnumerator GetAllStaffByRole_CO(string role, Action<bool, List<StaffData>> callback)
    {
        var payload = new GetAllEmployeeByRolePayload(role);
        var json = JsonConvert.SerializeObject(payload);
        var request = BackendCommunicator.CreatePostRequest(GET_ALL_EMPLOYEE_INFO_BY_ROLE_PATH, json);
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result is not (UnityWebRequest.Result.Success or UnityWebRequest.Result.ProtocolError
            ))
        {
            callback?.Invoke(false, null);
            yield break;
        }

        var staffData = JsonConvert.DeserializeObject<List<StaffData>>(request.downloadHandler.text);

        callback?.Invoke(true, staffData);
    }
}

public class GetAllEmployeeByRolePayload
{
    [JsonProperty("role")] public string Role { get; set; }

    public GetAllEmployeeByRolePayload(string role)
    {
        Role = role;
    }
}