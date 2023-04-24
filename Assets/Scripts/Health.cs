using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    int hitPoints;
    [SerializeField] int maxHitPoints = 3;
    bool canHit = true;
    [SerializeField] float invincibleTimeLength = 2.5f;
    [SerializeField] TextMeshProUGUI healthText;
    private void Awake()
    {
        hitPoints = maxHitPoints;
        UpdateHealthUI();
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

    IEnumerator EnableInvincibility()
    {
        canHit= false;
        for(float i = 0; i < invincibleTimeLength; i += 0.2f)
        {
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        canHit= true;
    }

    private void Die()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
