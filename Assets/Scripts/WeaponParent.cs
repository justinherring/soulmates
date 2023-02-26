using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponParent : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }

    public Bullet bullet;
    public Transform bulletTransform;

    private bool canFire = true;
    private float timer;

    [SerializeField]
    private float timeBetweenFiring;

    private void Update()
    {
        Vector2 direction = (Pointerposition - (Vector2)transform.position).normalized;

        // this rotates the actual weapon
        transform.right = direction;

        // this flips the weapon when the character is facing a different direction
        Vector2 scale = transform.localScale;
        if(direction.x < 0)
        {
            scale.y = -1;
        } 
        else
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        // this manages cooldown between firing
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                timer = 0;
                canFire = true;
            }
        }

    }

    public void Shoot()
    {
        if (canFire)
        {
            canFire = false;
            Quaternion direction = transform.rotation;
            GameObject b = Instantiate(bullet.gameObject, bulletTransform.position, direction);
        }
    }
}
