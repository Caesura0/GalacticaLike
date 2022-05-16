using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider healthSlider;

    Health playerHealth;
    ScoreKeeper playerScore;

    

    private void Awake()
    {
        playerScore = FindObjectOfType<ScoreKeeper>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>() ;
    }

    private void Start()
    {
        //i'd rather have a max health and a current health, lets refactor this later
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    private void Update()
    {
        scoreText.text = playerScore.GetCurrentScore().ToString("0000000000");
        healthSlider.value = playerHealth.GetHealth();
    }

}
