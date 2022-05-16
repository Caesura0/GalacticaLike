using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int currentScore;


    static ScoreKeeper instance;

    private void Awake()
    {
        instance = this;
    }



    private void ManageSingleton()
    {


        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void SetScore(int points)
    {
        currentScore += points;
    }

    public void ResetScore()
    {
        //maybe we can save score per level, reset it on death to the amount ended on last level?
        currentScore = 0;
    }

}
