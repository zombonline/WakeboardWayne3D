using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem trailParticles, splashParticles;

    bool playerGrounded,playerGroundedLastFrame;
    Transform player;

    private void Awake()
    {
        player = FindObjectOfType<Movement>().transform;
    }

    private void Update()
    {
        if(!player.GetComponent<Movement>().isGrinding && player.GetComponent<Movement>().isGrounded)
        {
            playerGrounded = true;
        }
        else
        {
            playerGrounded= false;
        }

        if(playerGrounded)
        {
            trailParticles.Play();
        }
        if(playerGrounded && !playerGroundedLastFrame)
        {
            splashParticles.transform.position = new Vector3(player.position.x, 0, player.position.z);
            splashParticles.Play();
        }
        if(!playerGrounded)
        {
            trailParticles.Stop();
        }

        playerGroundedLastFrame = playerGrounded;
    }



}
