using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        PlayerScore playerScore = other.GetComponent<PlayerScore>();
        if (playerScore != null)
        {
            Debug.Log("Add Player Score Increase Score");
            playerScore.IncreaseScore(1);
        }

        ScoreBoard playerScoreBoard = FindObjectOfType<ScoreBoard>();
        if (playerScoreBoard != null)
        {
            Debug.Log("Update ScoreBoard on CollectibleItem");
            playerScoreBoard.UpdateScoreboard();
        }
    }
}
