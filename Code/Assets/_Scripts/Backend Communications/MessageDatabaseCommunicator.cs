using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class MessageDatabaseCommunicator : MonoBehaviour
{
    private const string ADD_MESSAGE_PATH = "/api/message/add";
    private const string GET_MESSAGE_PATH = "/api/message";

    public void AddMessage(MessageData messageData, Action<bool> callback)
    {
        StartCoroutine(AddMessage_CO(messageData, callback));
    }

    private IEnumerator AddMessage_CO(MessageData messageData, Action<bool> callback)
    {
        string messageDataJson = JsonConvert.SerializeObject(messageData);

        var request = BackendCommunicator.CreatePostRequest(ADD_MESSAGE_PATH, messageDataJson);
        yield return request.SendWebRequest();

        Debug.Log("Send message: " + request.downloadHandler.text);
        if (request.result != UnityWebRequest.Result.Success)
        {
            callback?.Invoke(false);
            yield break;
        }

        callback?.Invoke(true);
    }

    public void GetMessage(string accountId, Action<bool, List<MessageData>> callback)
    {
        StartCoroutine(GetMessage_CO(accountId, callback));
    }

    private IEnumerator GetMessage_CO(string accountId, Action<bool, List<MessageData>> callback)
    {
        MessageRequestPayload payload =
            new() {User1 = AccountManager.Instance.AccountID, User2 = accountId};
        var payloadJson = JsonConvert.SerializeObject(payload);

        var request = BackendCommunicator.CreatePostRequest(GET_MESSAGE_PATH, payloadJson);
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if (request.result is not UnityWebRequest.Result.Success or UnityWebRequest.Result.ProtocolError)
        {
            callback?.Invoke(false, null);
            yield break;
        }

        var messageList = JsonConvert.DeserializeObject<List<MessageData>>(request.downloadHandler.text);
        callback?.Invoke(true, messageList);
    }
}

public class MessageRequestPayload
{
    [JsonProperty("user1")] public string User1 { get; set; }

    [JsonProperty("user2")] public string User2 { get; set; }
}