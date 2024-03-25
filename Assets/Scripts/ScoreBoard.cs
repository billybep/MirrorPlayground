using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public GameObject scoreboardPanel; // Panel yang berisi scoreboard
    public TMP_Text scoreboardText; // Teks untuk menampilkan skor

    private bool isTabPressed = false;

    void Start()
    {
        // Pastikan panel scoreboard dimatikan saat permainan dimulai
        scoreboardPanel.SetActive(false);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isTabPressed = true;
        }

        // Jika tombol "Tab" dilepaskan, atur isTabPressed menjadi false dan sembunyikan scoreboard
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            isTabPressed = false;
            HideScoreboard();
        }

        if (isTabPressed)
        {
            ShowScoreboard();
        }
        else
        {
            HideScoreboard();
        }

        // Tampilkan atau sembunyikan scoreboard saat tombol tab ditekan
        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     ToggleScoreboard();
        // }
    }

    // Fungsi untuk menampilkan scoreboard
    void ShowScoreboard()
    {
        UpdateScoreboard();
        scoreboardPanel.SetActive(true);
    }

    // Fungsi untuk menyembunyikan scoreboard
    void HideScoreboard()
    {
        UpdateScoreboard();
        scoreboardPanel.SetActive(false);
    }

    void ToggleScoreboard()
    {
        scoreboardPanel.SetActive(!scoreboardPanel.activeSelf); // Toggle keadaan aktif scoreboard
        if (scoreboardPanel.activeSelf)
        {
            UpdateScoreboard();
        }
    }

    public void UpdateScoreboard()
    {
        PlayerScore[] playerScores = FindObjectsOfType<PlayerScore>();

        System.Array.Sort(playerScores, (x, y) => y.score.CompareTo(x.score));

        string scoreboardInfo = "";

        foreach (PlayerScore playerScore in playerScores)
        {
            string playerName = playerScore.GetComponent<PlayerScript>().GetName();

            if (playerScore.isLocalPlayer) scoreboardInfo += $"<b><color=blue>{playerName}</color></b>: {playerScore.score}\n";
            else scoreboardInfo += $"{playerName}: {playerScore.score}\n";
        }

        scoreboardText.text = scoreboardInfo;
    }

    
}
