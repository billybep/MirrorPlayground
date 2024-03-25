using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerScore : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnScoreChanged))]
    public int score = 0;

    public GameObject scoreUI;
    public Text scoreText;


    void Start()
    {
        if (!isLocalPlayer)
        {
            // Sembunyikan UI jika bukan pemain lokal
            scoreUI.SetActive(false);
        }
        else
        {
            UpdateScoreUI(); // Memperbarui UI skor saat memulai objek pemain lokal
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (isLocalPlayer) // Hanya aktifkan UI jika ini adalah pemain lokal
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score.ToString();
            }
        }
    }

    private void OnScoreChanged(int oldScore, int newScore)
    {
        UpdateScoreUI();
    }

    // Dipanggil untuk menambahkan score, baik di sisi server maupun di sisi klien
    public void AddScore(int value)
    {
        if (isServer) // Tambahkan score di sisi server
        {
            score += value;
        }
    }
}
