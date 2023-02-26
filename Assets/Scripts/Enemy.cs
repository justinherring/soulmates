using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Enemy : MonoBehaviour
{

    private Animator animator;

    public HealthBar healthBar;

    public TextMeshProUGUI chatbubble;

    public ShootFireballs shootFireballs;

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
                shootFireballs.isAlive = false;
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

    public void RemoveBoss()
    {
        healthBar.enabled = false;
        gameObject.SetActive(false);
        if (chatbubble != null)
        {
            chatbubble.enabled = true;
        }
        Invoke("BossDefeated", 5);
    }

    private void BossDefeated()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(3);
    }
}
