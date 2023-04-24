using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    float timer = 1f;
    public float score;
    float scoreDisplayText;
    bool textCoroutineActive = false;

    private void Awake()
    {
        
    }

    public void AddScore(float amount)
    {
        score += amount;
        if(!textCoroutineActive)
        {
            StartCoroutine(UpdateScoreText());
        }
    }

    IEnumerator UpdateScoreText()
    {
        textCoroutineActive = true;
        while (score > scoreDisplayText)
        {
            var differenceSize = (((int)Math.Round(score, 3) - (int)Math.Round(scoreDisplayText, 3)) / 100) * 3;
            Debug.Log(differenceSize);
            scoreDisplayText += 1f + differenceSize ;
            if(scoreDisplayText > score)
            {
                scoreDisplayText= score;
            }
            scoreText.text = scoreDisplayText.ToString("0000000");
            var waitTime = .001f / (score / scoreDisplayText);
            yield return new WaitForSeconds(waitTime);
        }
        textCoroutineActive= false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            AddScore(1f);
            timer = 1f;
        }
    }

}
