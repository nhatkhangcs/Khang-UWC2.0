using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class MCPAPICommunicator : MonoBehaviour
{
    private const string GET_MCP_CURRENT_STATE_PATH = "/api/mcp/current/{0}";
    private const string GET_MCP_CURRENT_STATE_PERCENTAGE_PATH = "/api/mcp/current/percentage/{0}";

    public void GetMCPStatePercentage(string mcpId, Action<bool, float> callback)
    {
        StartCoroutine(GetMCPStatePercentage_CO(mcpId, callback));
    }

    private IEnumerator GetMCPStatePercentage_CO(string mcpId, Action<bool, float> callback)
    {
        var request =
            BackendCommunicator.CreateGetRequest(string.Format(GET_MCP_CURRENT_STATE_PERCENTAGE_PATH,
                mcpId));
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.ProtocolError)
        {
            callback?.Invoke(false, -1);
            yield break;
        }

        var mcpPercentage = JsonConvert.DeserializeObject<MCPPercentage>(request.downloadHandler.text);
        callback?.Invoke(true, (float)mcpPercentage.Percentage);
    }
}

public class MCPPercentage
{
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("percentage")] public double Percentage { get; set; }
}