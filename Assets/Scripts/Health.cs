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

    public void TakeDamage()
    {
        if(!canHit)
        {
            return;
        }

        hitPoints--;
        UpdateHealthUI();
        if(hitPoints <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(EnableInvincibility());
        }
    }

    public void RefreshHealth()
    {
        hitPoints = maxHitPoints;
    }

    IEnumerator EnableInvincibility()
    {
        canHit= false;
        for(float i = 0; i < invincibleTimeLength; i += 0.2f)
        {
            mesh.enabled = false;
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
