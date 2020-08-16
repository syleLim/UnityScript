﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private GameObject deathChunkParticle, deathBloodParticle;
    public float currentHealth;
    
    
    private GameManager GM;

    private void Start() {
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DecreaseHealth(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0.0f)
            Die();
    }    

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        GM.Respawn();
        Destroy(gameObject);
    }
}