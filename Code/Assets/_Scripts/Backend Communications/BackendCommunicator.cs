using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapbox.Json;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.Networking;

public class BackendCommunicator : PersistentSingleton<BackendCommunicator>
{
    private const string MAIN_PATH = "http://localhost:3125";

    private const string CREATE_ACCOUNT_PATH = "/api/auth/createAccount";
    private const string LOGIN_PATH = "/api/auth/login";
    private const string LOGOUT_PATH = "/api/auth/logout";

    private const string ASSIGN_WAYPOINTS_TO_COLLECTOR_PATH = "/api/map/waypoints";
    private const string GET_ALL_COLLECTOR_POSITION_PATH = "/api/map/allCurrentPosition";


    public StaffDatabaseCommunicator StaffDatabaseCommunicator;
    public MCPDatabaseCommunicator MCPDatabaseCommunicator;
    public VehicleDatabaseCommunicator VehicleDatabaseCommunicator;
    public TaskDatabaseCommunicator TaskDatabaseCommunicator;
    public MessageDatabaseCommunicator MessageDatabaseCommunicator;

    public MapAPICommunicator MapAPICommunicator;
    public MCPAPICommunicator MCPAPICommunicator;

    public void RequestLogin(string username, string password, Action<bool, LoginToken> callback)
    {
        StartCoroutine(Login_CO(username, password, callback));
    }

    // private IEnumerator Login_CO(string username, string password, Action<bool, LoginToken> callback)
    // {
    //     var user = new UserData {username = username, password = password};

    //     string json = JsonUtility.ToJson(user);

    //     var request = CreatePostRequest(LOGIN_PATH, json, false);
    //     yield return request.SendWebRequest();

    //     Debug.Log(request.downloadHandler.text);
    //     if (request.result != UnityWebRequest.Result.Success)
    //     {
    //         callback?.Invoke(false, null);
    //         yield break;
    //     }

    //     var loginToken = JsonConvert.DeserializeObject<LoginToken>(request.downloadHandler.text);
    //     callback?.Invoke(true, loginToken);
    //     request.Dispose();
    // }

    private IEnumerator Login_CO(string username, string password, Action<bool, LoginToken> callback)
    {
        var user = new UserData {username = username, password = password};

        string json = JsonUtility.ToJson(user);

        var request = CreatePostRequest(LOGIN_PATH, json);
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result != UnityWebRequest.Result.Success)
        {
            callback?.Invoke(false, null);
            yield break;
        }

        var loginToken = JsonConvert.DeserializeObject<LoginToken>(request.downloadHandler.text);
        callback?.Invoke(true, loginToken);
        request.Dispose();
    }

    private IEnumerator AssignWaypointsToCollector(string staffId, List<Vector2d> waypoints)
    {
        List<Coordinate> points = new();
        foreach (var waypoint in waypoints)
        {
            points.Add(new Coordinate(waypoint.x, waypoint.y));
        }

        var task = new CollectorRouteData() {CollectorId = staffId, Route = points};
        var taskJson = JsonUtility.ToJson(task);

        var request = CreatePostRequest(ASSIGN_WAYPOINTS_TO_COLLECTOR_PATH, taskJson);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) Debug.LogWarning("Request failed.");
    }

    private IEnumerator GetAllCollectorPosition(Action<bool, List<CollectorRouteData>> callback)
    {
        var request = CreateGetRequest(GET_ALL_COLLECTOR_POSITION_PATH);
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.ProtocolError)
        {
            callback?.Invoke(false, null);
            yield break;
        }

        var collectorsRouteData =
            JsonUtility.FromJson<List<CollectorRouteData>>(request.downloadHandler.text);

        callback?.Invoke(true, collectorsRouteData);
    }

    public static UnityWebRequest CreateGetRequest(string apiPath)
    {
        Debug.Log("Creating request: " + MAIN_PATH + apiPath);
        var request = new UnityWebRequest(MAIN_PATH + apiPath, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        //request.SetRequestHeader("Authorization", "Bearer " + AccountManager.Instance.GetAccessToken());
        return request;
    }

    public static UnityWebRequest CreatePostRequest(string apiPath, string json)
    {
        
        Debug.Log("Creating request: " + MAIN_PATH + apiPath + "\nPayload: " + json);
        var request = new UnityWebRequest(MAIN_PATH + apiPath, "POST");
        
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        //Debug.Log(needToken + "\n");
        request.downloadHandler = new DownloadHandlerBuffer();
        //Debug.Log("readched here \n");
        
        request.SetRequestHeader("Content-Type", "application/json");
        
        // if (needToken)
        //     request.SetRequestHeader("Authorization",
        //         "Bearer " + AccountManager.Instance.GetAccessToken());
        return request;
    }
}

public class UserData
{
    public string username;
    public string password;
}