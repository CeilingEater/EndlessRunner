using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    
    public void SubmitScore(string playerName, int score)
    {
        Debug.Log($"Attempting to submit: {playerName} - {score}");
        StartCoroutine(SubmitScoreCoroutine(playerName, score));
    }

    private IEnumerator SubmitScoreCoroutine(string name, int score)
    {
        ScoreData data = new ScoreData(name, score);
        string json = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest("http://localhost:3000/leaderboards", "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        
        
        Debug.Log($"Submitting score: {name} - {score}"); //pls work

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score submitted!");
            //loading after added to database
            SceneManager.LoadScene("Leaderboard");
        }
        else
        {
            Debug.LogError("Failed to submit score: " + request.error);
        }
    }

    [System.Serializable]
    private class ScoreData
    {
        public string playerName;
        public int score;

        public ScoreData(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }
}
