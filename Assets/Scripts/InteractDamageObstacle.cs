using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDamageObstacle : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        if (player.GetComponent<Health>().TakeDamage())
        {
            Destroy(gameObject);
        }
    }
}
