using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardDisplay : MonoBehaviour
{
    public TextMeshProUGUI playerNamesText; // Assign in Inspector
    public TextMeshProUGUI playerScoresText; // Assign in Inspector

    void Start()
    {
        StartCoroutine(GetLeaderboard());
    }

    IEnumerator GetLeaderboard()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/leaderboards");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = "{\"entries\":" + request.downloadHandler.text + "}";
            LeaderboardList leaderboard = JsonUtility.FromJson<LeaderboardList>(json);

            playerNamesText.text = "";
            playerScoresText.text = "";

            foreach (var entry in leaderboard.entries)
            {
                playerNamesText.text += $"{entry.player_name}\n";
                playerScoresText.text += $"{entry.score}\n";
            }
        }
        else
        {
            Debug.LogError("Error fetching leaderboard: " + request.error);
        }
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string player_name;
        public int score;
    }

    [System.Serializable]
    public class LeaderboardList
    {
        public List<LeaderboardEntry> entries;
    }
}