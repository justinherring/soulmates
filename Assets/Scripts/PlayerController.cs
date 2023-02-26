using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;

    private Vector2 mousePos;

    private Rigidbody2D rb;

    private WeaponParent weaponParent;

    private Animator animator;

    [SerializeField]
    private float health;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private ContactFilter2D movementFilter;

    [SerializeField]
    private float collisionOffset = 0.0f;

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private bool isAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero && isAlive)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
            }

            if (movementInput == Vector2.zero)
            {
                animator.SetInteger("walkDirection", 0);
            }
            else if (Mathf.Abs(movementInput.x) >= Mathf.Abs(movementInput.y) && movementInput.x >= 0)
            {
                animator.SetInteger("walkDirection", 4);
            }
            else if (Mathf.Abs(movementInput.x) >= Mathf.Abs(movementInput.y) && movementInput.x < 0)
            {
                animator.SetInteger("walkDirection", 3);
            }
            else if (Mathf.Abs(movementInput.x) < Mathf.Abs(movementInput.y) && movementInput.y >= 0)
            {
                animator.SetInteger("walkDirection", 2);
            }
            else if (Mathf.Abs(movementInput.x) < Mathf.Abs(movementInput.y) && movementInput.y < 0)
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

        if (count == 0 || true)
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
        if (isAlive)
        {
            weaponParent.Pointerposition = Camera.main.ScreenToWorldPoint(pointerPos);
        }
    }

    void OnFire()
    {
        if (isAlive)
        {
            weaponParent.Shoot();
        }
    }

    public float Health
    {
        set
        {
            health = value;
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

    public void Defeated()
    {
        isAlive = false;
        animator.SetTrigger("PlayerDefeated");
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}
