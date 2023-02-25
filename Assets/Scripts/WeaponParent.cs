using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }

    private void Update()
    {
        // this rotates the actual weapon
        transform.right = (Pointerposition - (Vector2)transform.position).normalized;
    }
}
