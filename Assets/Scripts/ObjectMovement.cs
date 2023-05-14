using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] public float currentSpeed, speed;
    [SerializeField] public float maxMultiplier, multiplierIncrease, multiplierIncreaseTime;
    float multiplier = 1;
    float timer;

    [SerializeField] TextMeshProUGUI SPEEDDISPLAY;

    private void Awake()
    {
        timer = multiplierIncreaseTime;

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
            }
        }
        SPEEDDISPLAY.text = (speed * multiplier).ToString() + "... multiplier = " + multiplier.ToString();
        transform.Translate(Vector3.forward * speed * multiplier * Time.deltaTime);
        if(transform.childCount == 0)
        {
            transform.position = Vector3.zero;
        }
    }
}
