using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RampTrickComboManager : MonoBehaviour
{
    [SerializeField] Sprite swipeUpImage, swipeDownImage, tapLeftImage, tapRightImage;
    [SerializeField] Image promptImage;
    [SerializeField] float targetCombo = 4f;

    [SerializeField] float fullComboScoreReward = 500;

    string BACKFLIP = "backFlip", FRONTFLIP = "frontFlip", RIGHTSPIN = "rightSpin", LEFTSPIN = "leftSpin";
    List<string> tricks = new List<string>();
    List<Sprite> trickSprites = new List<Sprite>();

    float currentCombo;
    string promptedTrick;

    private void Awake()
    {
        promptImage.enabled = false;
       tricks.Add(BACKFLIP); tricks.Add(FRONTFLIP); tricks.Add(RIGHTSPIN); tricks.Add(LEFTSPIN);
        trickSprites.Add(swipeUpImage); trickSprites.Add(swipeDownImage); trickSprites.Add(tapRightImage); trickSprites.Add(tapLeftImage);
    }
    public void BeginCombo()
    {
        currentCombo = 0;
        NewTrickPrompt();
    }

    private void NewTrickPrompt()
    {
        var randomTrickNumber = Random.Range(0, tricks.Count);
        promptedTrick = tricks[randomTrickNumber];
        promptImage.sprite = trickSprites[randomTrickNumber];
        promptImage.enabled = true;
    }

    public void TrickPeformed(string trickPerformed)
    {
        if(trickPerformed == promptedTrick)
        {
            currentCombo++;
            NewTrickPrompt();
            if(currentCombo == targetCombo)
            {
                promptImage.enabled = false;
                promptedTrick = "null";
            }
        }
        else
        {
            //animation to bring attention to the prompt
        }
    }

    public void EndCombo()
    {
        promptImage.enabled = false;
        FindObjectOfType<Score>().AddScore((fullComboScoreReward * (currentCombo / targetCombo)));
        Debug.Log(currentCombo);
        Debug.Log((fullComboScoreReward * (currentCombo / targetCombo)));
        promptedTrick = "null";
        currentCombo = 0;
    }

}
