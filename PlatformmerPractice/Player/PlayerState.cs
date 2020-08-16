using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private GameObject deathChunkParticle, deathBloodParticle;
    
    public float currentHealth;

    private void Start() {
        currentHealth = maxHealth;
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
        Destroy(gameObject);
    }
}
