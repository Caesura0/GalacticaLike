using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper playerScore;

    private void Awake()
    {
        playerScore = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        scoreText.text = "You Scored: \n " + playerScore.GetCurrentScore().ToString();
    }

}
