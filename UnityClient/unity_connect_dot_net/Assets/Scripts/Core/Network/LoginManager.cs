using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class LoginManager
{
    private static readonly string baseUrl = "http://localhost:5034"; // ✅ ใช้ HTTP แทน HTTPS

    public static async Task<string> Register(string username, string password)
    {
        var user = new UserData(username, password);
        return await Post("api/Auth/register", user);
    }

    public static async Task<string> Login(string username, string password)
    {
        var user = new UserData(username, password);
        return await Post("api/Auth/login", user);
    }

    private static async Task<string> Post(string endpoint, object data)
    {
        string json = JsonUtility.ToJson(data);
        string fullUrl = $"{baseUrl}/{endpoint}";

        // ✅ Debug URL และ JSON ที่จะส่ง
        Debug.Log($"📤 Sending POST to: {fullUrl}");
        Debug.Log($"📦 Payload: {json}");

        using UnityWebRequest request = new UnityWebRequest(fullUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.certificateHandler = new BypassCertificate(); //TODO

        var operation = request.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"✅ Success! Response: {request.downloadHandler.text}");
            return request.downloadHandler.text;
        }
        else
        {
            Debug.LogError($"❌ Error: {request.error}");
            return $"Error: {request.error}";
        }
    }

    [Serializable]
    private class UserData
    {
        public string Username;
        public string Password;

        public UserData(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData) => true;
    }
}
