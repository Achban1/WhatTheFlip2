using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovementParticle : MonoBehaviour
{
    [SerializeField] UnityEngine.ParticleSystem movementParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;

    PlayerMovement playerMovement;

    float counter;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        counter += Time.deltaTime;

        if (Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod && playerMovement.onGround == true)

            {
                movementParticle.Play();       
                counter = 0;
            }
        }
    }
}
