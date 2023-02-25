using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;

    private Vector2 mousePos;

    private Rigidbody2D rb;

    private WeaponParent weaponParent;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private ContactFilter2D movementFilter;

    [SerializeField]
    private float collisionOffset = 0.0f;

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponParent = GetComponentInChildren<WeaponParent>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            // this the player slide against the wall if moving diagonally
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success)
                {
                    TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        // check for potential collisions
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movementInput);
            return true;
        }

        return false;
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnLook(InputValue lookValue)
    {
        mousePos = lookValue.Get<Vector2>();
        Vector3 pointerPos = new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane);
        weaponParent.Pointerposition = Camera.main.ScreenToWorldPoint(pointerPos);
    }
}
