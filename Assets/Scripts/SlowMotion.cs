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
        
        if (speedPercentage == 100f)
        {
            speedPercentage = 50f;
            StartCoroutine(EffectFactors());
        }
        else
        {
            speedPercentage = 50f;
        }
    }

    IEnumerator EffectFactors()
    {
        while (speedPercentage < 100f)
        {
            speedPercentage += 1f;
            GameObject.FindWithTag("Player").GetComponent<Movement>().downwardVelocityCap = normalGravityScale * (speedPercentage / 200);
            FindObjectOfType<ObjectMovement>().speed = FindObjectOfType<ObjectMovement>().currentSpeed * (speedPercentage/200);
            foreach(Water waterPlane in FindObjectsOfType<Water>())
            {
                waterPlane.scrollY = waterPlane.scrollSpeed * (speedPercentage / 200);
            }
            FindObjectOfType<ObstacleSpawner>().timerEffector = (speedPercentage / 100);
            yield return new WaitForSeconds(.015f);

            if(GameObject.FindWithTag("Player").GetComponent<Movement>().isGrounded)
            {
                speedPercentage = 100f;
            }
        }
        FindObjectOfType<ObjectMovement>().speed = FindObjectOfType<ObjectMovement>().currentSpeed;
        GameObject.FindWithTag("Player").GetComponent<Movement>().downwardVelocityCap = normalGravityScale;
        FindObjectOfType<ObstacleSpawner>().timerEffector = 1f;

        foreach (Water waterPlane in FindObjectsOfType<Water>())
        {
            waterPlane.scrollY = waterPlane.scrollSpeed;
        }

        yield return null;
    }

}
