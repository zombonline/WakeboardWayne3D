using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
