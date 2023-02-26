using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Animator animator;

    public HealthBar healthBar;

    public float Health
    {
        set
        {
            health = value;
            if (healthBar != null)
            {
                healthBar.SetHealth(health);
            }
            if (health <= 0)
            {
                Defeated();
            }
        }

        get
        {
            return health;
        }
    }

    public float health = 10;
    public float maxHealth = 10;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    void Update()
    {
        
    }

    public void Defeated()
    {
        animator.SetTrigger("EnemyDefeated");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
