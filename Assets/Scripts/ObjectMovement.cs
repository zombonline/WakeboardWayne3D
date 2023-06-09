using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] public float startSpeed, speed;
    [SerializeField] public float maxMultiplier, multiplierIncrease, multiplierIncreaseTime;
    float multiplier = 1;
    float timer;

    public bool ignoreMultiplier = false;


    private void Awake()
    {
        timer = multiplierIncreaseTime;
        speed = startSpeed * multiplier;
    }

    private void Update()
    {
        if (multiplier < maxMultiplier)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = multiplierIncreaseTime;
                multiplier += multiplierIncrease;
                speed = startSpeed * multiplier;
            }
        }
        if (ignoreMultiplier)
        {
            transform.Translate(Vector3.forward * startSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
