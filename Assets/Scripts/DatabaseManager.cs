using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class DatabaseManager : MonoBehaviour
{
    /*public delegate void DatabaseUpdateDelegate();
    public static event DatabaseUpdateDelegate OnDatabaseUpdate;
    
    public static DatabaseManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        
        instance = this;
    }

    public void Post(string endpoint, string json)
    {
        StartCoroutine(PostData(endpoint, json));
    }

    private IEnumerator PostData(string endpoint, string json)
    {
        string URL = GameManager.instance.DatabaseURL + endpoint;
        UnityWebRequest www = new UnityWebRequest(URL, "POST");
        
        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(body);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Object added to database");
            Debug.Log(www.downloadHandler.text);
            OnDatabaseUpdate?.Invoke();
        }
    }

    public void Get(string endpoint, Action<string> callback = null)
    {
        StartCoroutine(GetData(endpoint, callback));
    }

    private IEnumerator GetData(string endpoint, Action<string> callback)
    {
        string URL = GameManager.instance.DatabaseURL + endpoint;
        UnityWebRequest www =  UnityWebRequest.Get(URL);
        
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
            callback(null);
        }
        else
        {
            Debug.Log("Object added to database");
            callback(www.downloadHandler.text);
        }
    }*/
}
