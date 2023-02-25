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

    private Animator animator;

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
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            Vector2 actualMove = MoveResult(movementInput);
            if (Mathf.Abs(actualMove.x) >= Mathf.Abs(actualMove.y) && actualMove.x >= 0)
            {
                animator.SetInteger("walkDirection", 4);
            }
            else if (Mathf.Abs(actualMove.x) >= Mathf.Abs(actualMove.y) && actualMove.x < 0)
            {
                animator.SetInteger("walkDirection", 3);
            }
            else if (Mathf.Abs(actualMove.x) < Mathf.Abs(actualMove.y) && actualMove.y >= 0)
            {
                animator.SetInteger("walkDirection", 2);
            }
            else if (Mathf.Abs(actualMove.x) < Mathf.Abs(actualMove.y) && actualMove.y < 0)
            {
                animator.SetInteger("walkDirection", 1);
            }
        }
        else
        {
            animator.SetInteger("walkDirection", 0);
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

    private Vector2 MoveResult(Vector2 direction)
    {
        bool success = TryMove(movementInput);

        // this the player slide against the wall if moving diagonally
        if (!success)
        {
            // try move in x first
            success = TryMove(new Vector2(movementInput.x, 0));
            if (!success)
            {
                // try move in y
                success = TryMove(new Vector2(0, movementInput.y));
                if (!success) {
                    return Vector2.zero;
                } 
                else
                {
                    return new Vector2(0, movementInput.y);
                }
            } 
            else
            {
                return new Vector2(movementInput.x, 0);
            }

        } else
        {
            return direction;
        }
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

    void OnFire()
    {
        weaponParent.Shoot();
    }
}
