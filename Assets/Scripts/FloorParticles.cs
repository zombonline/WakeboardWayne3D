using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem trailParticles, splashParticles, grindParticles;

    bool playerGrounded, playerGrinding ,playerGroundedLastFrame;
    Transform player;

    private void Awake()
    {
        player = FindObjectOfType<Movement>().transform;
        Debug.Log(gameObject.name);
    }

    private void Update()
    {
        playerGrounded = player.GetComponent<Movement>().isGrounded;
        playerGrinding = player.GetComponent<Movement>().isGrinding;
        

        if(playerGrounded && !playerGrinding)
        {
            trailParticles.Play();
        }
        if(playerGrounded && !playerGrinding && !playerGroundedLastFrame)
        {
            splashParticles.transform.position = new Vector3(player.position.x, 0, player.position.z);
            splashParticles.Play();
        }
        if(!playerGrounded)
        {
            trailParticles.Stop();
        }
        if(playerGrinding && playerGrounded)
        {
            grindParticles.Play();
        }
        else
        {
            grindParticles.Stop();
        }

        playerGroundedLastFrame = playerGrounded;
    }



}
