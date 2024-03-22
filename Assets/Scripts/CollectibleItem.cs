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
            playerScore.IncreaseScore(1);
        }
    }
}
