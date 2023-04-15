using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;


public class TaskDatabaseCommunicator : MonoBehaviour
{
    private const string ADD_TASK_PATH = "/api/task/add";
    private const string GET_TASK_INFO_PATH = "/api/task/info/{0}";
    private const string GET_EMPLOYEE_TASKS_PATH = "/api/task/info/employee/{0}";

    public void AddTask(AddTaskPayload taskData, Action<bool> callback)
    {
        StartCoroutine(AddTask_CO(taskData, callback));
    }

    private IEnumerator AddTask_CO(AddTaskPayload taskData, Action<bool> callback)
    {
        string taskDataDataJson = JsonConvert.SerializeObject(taskData);

        var request = BackendCommunicator.CreatePostRequest(ADD_TASK_PATH, taskDataDataJson);
        yield return request.SendWebRequest();

        Debug.Log("Adding task: " + request.downloadHandler.text);
        if (request.result != UnityWebRequest.Result.Success)
        {
            callback?.Invoke(false);
            yield break;
        }

        callback?.Invoke(true);
    }
    
    public void GetTaskInfo(string taskId, Action<bool, TaskData> callback)
    {
        StartCoroutine(GetTaskInfo_CO(taskId, callback));
    }

    private IEnumerator GetTaskInfo_CO(string taskId, Action<bool, TaskData> callback)
    {
        var request = BackendCommunicator.CreateGetRequest(string.Format(GET_TASK_INFO_PATH, taskId));
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.ProtocolError)
        {
            callback?.Invoke(false, null);
            yield break;
        }

        var taskData = JsonConvert.DeserializeObject<TaskData>(request.downloadHandler.text);
        callback?.Invoke(true, taskData);
    }

    public void GetAllTasksOfEmployee(string employeeId, Action<bool, List<TaskData>> callback)
    {
        StartCoroutine(GetAllTasksOfEmployee_CO(employeeId, callback));
    }

    private IEnumerator GetAllTasksOfEmployee_CO(string employeeId,
        Action<bool, List<TaskData>> callback)
    {
        var request =
            BackendCommunicator.CreateGetRequest(string.Format(GET_EMPLOYEE_TASKS_PATH, employeeId));
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.ProtocolError)
        {
            callback?.Invoke(false, null);
            yield break;
        }

        var taskData = JsonConvert.DeserializeObject<List<TaskData>>(request.downloadHandler.text);
        callback?.Invoke(true, taskData);
    }

    public void GetAllTaskOfEmployeeByDate(string employeeId, DateTime date,
        Action<bool, List<TaskData>> callback)
    {
        StartCoroutine(GetAllTasksOfEmployee_CO(employeeId, (isSucceeded, list) =>
        {
            if (!isSucceeded)
            {
                callback?.Invoke(false, null);
                return;
            }

            List<TaskData> filteredList = new();

            foreach (var taskData in list)
            {
                var timestamp = DateTime.Parse(taskData.Timestamp);
                //if (timestamp.Date == date)
                    filteredList.Add(taskData);
            }

            callback?.Invoke(true, filteredList);
        }));
    }
}

public class AddTaskPayload
{
    [JsonProperty("employee_id")]
    public string EmployeeID { get; set; }

    [JsonProperty("mcp_id")]
    public string MCPID { get; set; }

    [JsonProperty("vehicle_id")]
    public string VehicleID { get; set; }

    [JsonProperty("timeToDo")]
    public string Timestamp { get; set; }

    [JsonProperty("checkin")]
    public int CheckedIn { get; set; }

    [JsonProperty("checkout")]
    public int CheckedOut { get; set; }
}

