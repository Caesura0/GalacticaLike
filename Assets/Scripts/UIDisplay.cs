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
        healthSlider.maxValue = playerHealth.GetHealthNormalized();
    }

    private void Update()
    {
        Debug.Log(playerHealth.GetHealthNormalized());
        //Make these events. id also like them to be more animated
        scoreText.text = playerScore.GetCurrentScore().ToString("0000000000");
        healthSlider.value = playerHealth.GetHealthNormalized();
    }

}
