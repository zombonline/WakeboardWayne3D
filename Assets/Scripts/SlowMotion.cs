using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    float normalGravityScale;
    float speedPercentage = 100f;

    private void Awake()
    {
        normalGravityScale = GameObject.FindWithTag("Player").GetComponent<Movement>().downwardVelocityCap;

    }

    public void ActivateSlowMotion()
    {
        
        if (speedPercentage >= 1f)
        {
            speedPercentage = .5f;
            StartCoroutine(EffectFactors());
        }
        else
        {
            speedPercentage = .5f;
        }
    }

    IEnumerator EffectFactors()
    {
        while (speedPercentage < 1f)
        {
            speedPercentage += .01f;
            /*
            GameObject.FindWithTag("Player").GetComponent<Movement>().downwardVelocityCap = normalGravityScale * (speedPercentage / 200);
            FindObjectOfType<ObjectMovement>().speed = FindObjectOfType<ObjectMovement>().currentSpeed * (speedPercentage/200);

            FindObjectOfType<ObstacleSpawner>().timerEffector = (speedPercentage / 100);
            yield return new WaitForSeconds(.015f);
            */

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
        yield return null;*/
    }

}
