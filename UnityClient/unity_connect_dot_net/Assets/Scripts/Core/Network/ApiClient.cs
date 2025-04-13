using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public static class ApiClient
{
    private static readonly string baseUrl = "http://localhost:5000/api/";

    public static IEnumerator Get(string endpoint, Action<string> onSuccess, Action<string> onError)
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + endpoint);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            onSuccess?.Invoke(request.downloadHandler.text);
        else
            onError?.Invoke(request.error);
    }

    public static IEnumerator Post<T>(string endpoint, T data, Action<string> onSuccess, Action<string> onError)
    {
        string json = JsonUtility.ToJson(data);
        UnityWebRequest request = new UnityWebRequest(baseUrl + endpoint, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            onSuccess?.Invoke(request.downloadHandler.text);
        else
            onError?.Invoke(request.error);
    }

    public static IEnumerator Put<T>(string endpoint, T data, Action<string> onSuccess, Action<string> onError)
    {
        string json = JsonUtility.ToJson(data);
        UnityWebRequest request = new UnityWebRequest(baseUrl + endpoint, "PUT");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            onSuccess?.Invoke(request.downloadHandler.text);
        else
            onError?.Invoke(request.error);
    }

    public static IEnumerator Delete(string endpoint, Action<string> onSuccess, Action<string> onError)
    {
        UnityWebRequest request = UnityWebRequest.Delete(baseUrl + endpoint);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            onSuccess?.Invoke(request.downloadHandler.text);
        else
            onError?.Invoke(request.error);
    }
}
