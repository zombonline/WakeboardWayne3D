using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText, scoreGainText;
    [SerializeField] float scoreGainTime = 5f;
    [SerializeField] int scoreGainMaxLines;
    float timer = 1f;
    public float score;
    float currentDisplayedScoreAmount;
    bool textCoroutineActive = false;
    float fontSize;
    private void Awake()
    {
        fontSize = scoreText.fontSize;
    }

    public void AddScore(int amount, string message = null)
    {
        score += amount;
        if(!textCoroutineActive)
        {
            StartCoroutine(UpdateScoreText());
        }

        if(message != null)
        {
            DisplayScoreGain(message, amount);
        }
    }

    IEnumerator UpdateScoreText()
    {
        textCoroutineActive = true;
        while (score > currentDisplayedScoreAmount)
        {
            scoreText.fontSize = fontSize + 5f;
            var differenceSize = (((int)Math.Round(score, 3) - (int)Math.Round(currentDisplayedScoreAmount, 3)) / 100) * 3;
            currentDisplayedScoreAmount += 1f + differenceSize ;
            if(currentDisplayedScoreAmount > score)
            {
                currentDisplayedScoreAmount= score;
            }
            scoreText.text = currentDisplayedScoreAmount.ToString("0000000");
            var waitTime = .001f / (score / currentDisplayedScoreAmount);
            yield return new WaitForSeconds(waitTime/2);
            scoreText.fontSize = fontSize + 2.5f;
            yield return new WaitForSeconds(waitTime / 2);

        }
        scoreText.fontSize = fontSize;
        textCoroutineActive= false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            AddScore(1);
            timer = 1f;
        }
    }

    void RemoveFirstLineOfScoreGainText()
    {
        int index = scoreGainText.text.IndexOf('\n');
        scoreGainText.text = scoreGainText.text.Substring(index + 1);
    }

    void DisplayScoreGain(string message, int score)
    {
        if(score <= 0)
        {
            return;
        }

        if(CheckIfMaxLinesReached())
        {
            RemoveFirstLineOfScoreGainText();
        }
        scoreGainText.text = scoreGainText.text + message + " - +" + score.ToString() + "\n";
        Invoke(nameof(RemoveFirstLineOfScoreGainText), scoreGainTime);
    }

    bool CheckIfMaxLinesReached()
    {
        var counter = 0;
        foreach(char c in scoreGainText.text)
        {
            if(c == '\n')
            {
                counter++;
            }
        }
        return counter >= scoreGainMaxLines;
    }
    

}
