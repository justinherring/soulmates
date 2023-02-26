using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D bulletCollider;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float deadzone;

    [SerializeField]
    private Sprite[] sprites;

    public float damage;

    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length)];

        bulletCollider = GetComponent<Collider2D>();

        rb = GetComponent<Rigidbody2D>();
        float theta = transform.eulerAngles.z;
        Vector2 direction = new Vector2(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad)).normalized;
        if (direction.x < 0)
        {
            sr.flipY = true;
        }
        rb.velocity = direction * bulletSpeed;
    }

    void Update()
    {
        if (transform.position.magnitude > deadzone)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            // deal damage
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }
}
