using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float deadzone;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

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
}
