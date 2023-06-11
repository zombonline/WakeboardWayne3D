using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    float normalGravityScale;
    float speedPercentage = 100f;

    private void Awake()
    {
        normalGravityScale = GameObject.FindWithTag("Player").GetComponent<Movement>().downwardVelocityCap;
    }

    public void ActivateSlowMotion(float desiredValue, float transitionTime)
    {
        StartCoroutine(TransitionTimeScale(desiredValue, transitionTime));

    }

    IEnumerator TransitionTimeScale(float desiredValue,float transitionTime)
    {
        if (desiredValue < Time.timeScale)
        {
            var incrementTime = (transitionTime / desiredValue - Time.timeScale) / 100;
            while (Time.timeScale != desiredValue)
            {
                if(!FindObjectOfType<TimeControl>().timePaused)
                {
                    Time.timeScale -= .01f;
                    if (desiredValue >= Time.timeScale)
                    {
                        Time.timeScale = desiredValue;
                    }
                }
                yield return new WaitForSecondsRealtime(incrementTime);

            }
        }
        else
        {
            var incrementTime = (transitionTime / desiredValue - Time.timeScale) / 100;
            while (Time.timeScale != desiredValue)
            {
                if (!FindObjectOfType<TimeControl>().timePaused)
                {
                    Time.timeScale += .01f;
                    if (desiredValue <= Time.timeScale)
                    {
                        Time.timeScale = desiredValue;
                    }
                }
                yield return new WaitForSecondsRealtime(incrementTime);

            }
        }
    }


    /*IEnumerator EffectFactors()
    {
        while (speedPercentage < 1f)
        {
            speedPercentage += .01f;
            /*
            GameObject.FindWithTag("Player").GetComponent<Movement>().downwardVelocityCap = normalGravityScale * (speedPercentage / 200);
            FindObjectOfType<ObjectMovement>().speed = FindObjectOfType<ObjectMovement>().currentSpeed * (speedPercentage/200);

            FindObjectOfType<ObstacleSpawner>().timerEffector = (speedPercentage / 100);
            yield return new WaitForSeconds(.015f);
            

            if(GameObject.FindWithTag("Player").GetComponent<Movement>().isGrounded)
            {
                speedPercentage = 1f;
            }
            Time.timeScale = speedPercentage;
            yield return new WaitForSecondsRealtime(.1f);
        }
        /*FindObjectOfType<ObjectMovement>().speed = FindObjectOfType<ObjectMovement>().currentSpeed;
        GameObject.FindWithTag("Player").GetComponent<Movement>().downwardVelocityCap = normalGravityScale;
        FindObjectOfType<ObstacleSpawner>().timerEffector = 1f;
        yield return null;
    }*/

}
