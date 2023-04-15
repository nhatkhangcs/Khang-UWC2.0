using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class MCPDatabaseCommunicator : MonoBehaviour
{
    private const string GET_MCP_INFO_PATH = "/api/mcp/{0}";
    private const string GET_ALL_MCP_INFO_PATH = "/api/mcp/all";

    public void GetAllMCP(Action<bool, List<MCPData>> callback)
    {
        StartCoroutine(GetAllMCP_CO(callback));
    }

    private IEnumerator GetAllMCP_CO(Action<bool, List<MCPData>> callback)
    {
        var request = BackendCommunicator.CreateGetRequest(GET_ALL_MCP_INFO_PATH);
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.ProtocolError)
        {
            callback?.Invoke(false, null);
            yield break;
        }

        var mcpData = JsonConvert.DeserializeObject<List<MCPData>>(request.downloadHandler.text);
        callback?.Invoke(true, mcpData);
    }
}