using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script is responsible for allowing the player to perform tricks when airborne from a ramp.
//It also will prompt the player to perform 3 random stunts and award score based on how many the player managed to perform before making a mistake or returning to the ground.
public class RampTrickComboManager : MonoBehaviour
{
    [SerializeField] Sprite swipeUpIcon, swipeDownIcon, doubleTapIcon; //icons to show on canvas so player knows what trick to perform.
    [SerializeField] Image promptImage; //reference to image component on which icons will be displayed.
    [SerializeField] int targetCombo = 3; //max amount of tricks in a single combo.

    [SerializeField] float fullComboScoreReward = 500; //max score that can be awarded from a full combo, this will be split based on how many tricks a player performed
                                                       //(I.E: Player does 2 tricks out of max 3, they will receive 2/3 of the max score at the end of their combo.)

    //generate two lists for trickAnimation string and prompt icon. These will match up by their index so a random number can retreive the correct string and icon to display.
    List<string> tricks = new List<string>(); 
    List<Sprite> trickSprites = new List<Sprite>();

    //current combo starts at 0 and increments each time player performs the correct trick, it will be used to check if player has finished full combo and to decide the amount of score to reward at end of combo.
    int currentCombo;
    //string reference to the currently prompted trick, can be used to compare the trick the player performs agianst what is being prompted
    string promptedTrick;

    Animator animator;
    Movement playerMovement;

    bool comboInProgess = false;

    private void Awake()
    {
        //reference to movement script to check isground/isairborne bools
        playerMovement = GetComponent<Movement>(); 
        
        //assign to both lists in correct order so indexes will match.
        tricks.Add("backFlip"); tricks.Add("frontFlip"); tricks.Add("doubleTap");
        trickSprites.Add(swipeUpIcon); trickSprites.Add(swipeDownIcon); trickSprites.Add(doubleTapIcon);
    }

    private void Start()
    {
        //animator is assigned in 'Start' as Wayne model (which holds animator component) is instantiated in 'Awake'.
        animator = GetComponentInChildren<Animator>();
    }
    //This method will be triggered by the Ramp Script.
    public void BeginCombo()
    {
        currentCombo = 0;
        NewTrickPrompt();
        comboInProgess = true;
    }
    public void EndCombo()
    {
        promptImage.enabled = false;

        string scoreMessage = currentCombo.ToString() + "/" + targetCombo.ToString() + " Combo";
        if(currentCombo == targetCombo)
        {
            scoreMessage = "Full Combo";
        }
        FindObjectOfType<Score>().AddScore((Mathf.RoundToInt(fullComboScoreReward * (currentCombo / targetCombo))),scoreMessage);
        promptedTrick = "null";
        currentCombo = 0;
        comboInProgess=false;
    }

    //This method will be triggered via Swipe Controls.
    public void RampTrick(string trickAnimation)
    {
        //Check if player is in the correct state to perform a trick. Airborne from a ramp and also not in another trick animation
        if (!playerMovement.airborneFromRamp || !animator.GetCurrentAnimatorStateInfo(0).IsName("Wayne_Falling"))
        {
            return;
        }
        //disable prompt image and trigger animation.
        promptImage.enabled = false;
        animator.SetTrigger(trickAnimation);
    }
    private void NewTrickPrompt()
    {
       
        var randomTrickNumber = Random.Range(0, tricks.Count);

        //use random number to assign prompted stunt string and also icon
        promptedTrick = tricks[randomTrickNumber];
        promptImage.sprite = trickSprites[randomTrickNumber];
        //display icon
        promptImage.enabled = true;
    }

    public void TrickPerformed(string trickPerformed)
    {
        if(trickPerformed == promptedTrick)
        {
            currentCombo++;
            if(currentCombo == targetCombo)
            {
                promptImage.enabled = false;
                promptedTrick = "null";
            }
            else
            {
                NewTrickPrompt();
            }
        }
        else
        {
            EndCombo();
        }
    }

    

}
