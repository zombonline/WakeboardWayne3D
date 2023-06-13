using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    int hitPoints;
    [SerializeField] int maxHitPoints = 3;
    bool canHit = true;
    [SerializeField] float invincibleTimeLength = 2.5f;
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] SkinnedMeshRenderer mesh;

    [SerializeField] UnityEvent onDeath;
    private void Awake()
    {
        hitPoints = maxHitPoints;
        UpdateHealthUI();
    }
    private void Start()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();    
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Hit Points: " + hitPoints + "/" + maxHitPoints;
    }

    public bool TakeDamage()
    {
        var returnValue = canHit;
        if (canHit)
        {
            hitPoints--;
            UpdateHealthUI();
            if (hitPoints <= 0)
            {
                Die();
            }
            else
            {
                EnableInvincibility(invincibleTimeLength, true);
            }
        }
        return returnValue;
    }

    public void RefreshHealth()
    {
        hitPoints = maxHitPoints;
    }
    public void EnableInvincibility(float duration, bool flashModel)
    {
        StartCoroutine(EnableInvincibilityCoroutine(invincibleTimeLength, flashModel ));
    }
    IEnumerator EnableInvincibilityCoroutine(float duration, bool flashModel)
    {
        canHit= false;
        for(float i = 0; i < duration; i += 0.2f)
        {
            if(flashModel)
            { mesh.enabled = false; }
            yield return new WaitForSeconds(0.1f);
           mesh.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        canHit= true;
    }

    private void Die()
    {
        onDeath.Invoke();
    }
}
