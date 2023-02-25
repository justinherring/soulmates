using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }

    private void Update()
    {
        Vector2 direction = (Pointerposition - (Vector2)transform.position).normalized;

        // this rotates the actual weapon
        transform.right = direction;

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
    }
}
