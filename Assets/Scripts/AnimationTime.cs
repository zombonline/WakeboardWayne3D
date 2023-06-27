using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script will allow animations running in unscaled time to be affected by scaled time being set to 0. To allow animations to pause.
public class AnimationTime : MonoBehaviour
{
    Animator animator;
    [SerializeField] bool pauseWithFixedTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!pauseWithFixedTime)
        {
            return;
        }
        if(Time.timeScale == 0)
        {
            animator.updateMode = AnimatorUpdateMode.Normal;
        }
        else
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }
}
