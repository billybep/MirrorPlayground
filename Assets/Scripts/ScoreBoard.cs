using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public GameObject scoreboardPanel; // Panel yang berisi scoreboard
    public TMP_Text scoreboardText; // Teks untuk menampilkan skor

    void Update()
    {
        // Tampilkan atau sembunyikan scoreboard saat tombol tab ditekan
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleScoreboard();
        }
    }

    void ToggleScoreboard()
    {
        scoreboardPanel.SetActive(!scoreboardPanel.activeSelf); // Toggle keadaan aktif scoreboard
        if (scoreboardPanel.activeSelf)
        {
            UpdateScoreboard();
        }
    }

    void UpdateScoreboard()
    {
        PlayerScore[] playerScores = FindObjectsOfType<PlayerScore>();

        System.Array.Sort(playerScores, (x, y) => y.score.CompareTo(x.score));

        string scoreboardInfo = "";

        foreach (PlayerScore playerScore in playerScores)
        {
            string playerName = playerScore.GetComponent<PlayerScript>().GetName();
            scoreboardInfo += $"{playerName}: {playerScore.score}\n";
        }

        scoreboardText.text = scoreboardInfo;
    }

    
}
