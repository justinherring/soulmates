using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D bulletCollider;

    private Vector2 fireballDirection;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float deadzone;

    public float damage;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    void Start()
    {

        sr = GetComponent<SpriteRenderer>();

        bulletCollider = GetComponent<Collider2D>();

        rb = GetComponent<Rigidbody2D>();
        float theta = transform.eulerAngles.z;
        Vector2 direction = new Vector2(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad)).normalized;
        if (direction.x < 0)
        {
            sr.flipY = true;
        }
        fireballDirection = direction;
    }

    void Update()
    {
        rb.velocity = fireballDirection * bulletSpeed;
    }

    public void SetMoveDirection(Vector2 direction)
    {
        fireballDirection = direction;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // deal damage
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Health -= damage;
            }
        }
    }
}
