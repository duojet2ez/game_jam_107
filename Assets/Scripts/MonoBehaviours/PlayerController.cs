#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Attributes")]

    [SerializeField]
    private float JUMP_FORCE;

    [SerializeField]
    private float groundCheckRadius;

    [SerializeField]
    private float SPEED_RANGE;

    [SerializeField]
    private float MAX_HEALTH;

    [SerializeField]
    private float PUNISHMENT_COOLDOWN;

    [HideInInspector]
    public bool notMoveRight = false;

    [HideInInspector]
    public bool notMoveLeft = false;

    [HideInInspector]
    public bool notJump = false;

    private bool facesRight = true;
    
    private float speedRange;

    private float health;

    private float jumpForce;

    private float punishmentCooldown;

    [Space]

    [Header("References")]

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform bulletSpawn;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Punishments punishments;

    private Punishment punishment;

    public GameManager gameManager;

    public GameObject bullet;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        speedRange = SPEED_RANGE;
        jumpForce = JUMP_FORCE;
        health = MAX_HEALTH;
    }

    private void Update()
    {
        HandleMovement();
        HandleTimers();
        HandleShooting();
        playerAnimator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        if(health <= 0)
        {
            Die();
        }
    }

    private void HandleMovement()
    {
        int moveX = 0;
            
        MoveLeft(ref moveX);
        MoveRight(ref moveX);
        Jump();

        if (rb.velocity.x < speedRange && rb.velocity.x > -speedRange)
        {
            rb.velocity += new Vector2(moveX, 0);
        }
    }

    private void MoveLeft(ref int moveX)
    {
        if(InputManager.instance.GetKey("moveLeft"))
        {
            moveX = -1;
            if (facesRight)
                Rotate();
            if(notMoveLeft == true)
            {
                if (punishmentCooldown <= 0)
                {
                    punishment = punishments.GivePunishment();
                    Punish();
                    Debug.Log("Disabled Key Pressed");
                }
            }
        }
    }

    private void MoveRight(ref int moveX)
    {
        if (InputManager.instance.GetKey("moveRight"))
        {
            moveX = 1;
            if (!facesRight)
                Rotate();
            if (notMoveRight == true)
            {
                if (punishmentCooldown <= 0)
                {
                    punishment = punishments.GivePunishment();
                    Punish();
                    Debug.Log("Disabled Key Pressed");
                }
            }
        }

    }

    private void Jump()
    {
        int moveX = 0;
        int moveY = 0;

        if (InputManager.instance.GetKeyDown("jump"))
        {
            bool isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            if (isGround)
            {
                moveY = 1;
                if (notMoveRight == true)
                {
                    moveX = 1;
                    if (!facesRight)
                    {
                        Rotate();
                    }
                }
                else if (notMoveLeft == true)
                {
                    moveX = -1;
                    if(facesRight)
                    {
                        Rotate();
                    }
                }
            }

            if (notJump == true)
            {
                if (punishmentCooldown <= 0)
                {
                    punishment = punishments.GivePunishment();
                    Punish();
                    Debug.Log("Disabled Key Pressed");
                }
            }

        }

        rb.velocity += new Vector2(moveX * (speedRange / 2), moveY * jumpForce);
    }
    private void Rotate()
    {
        facesRight = !facesRight;

        Vector3 rotation = new Vector3(0, 180, 0);

        transform.Rotate(rotation);
    }

    private void Punish()
    {
        speedRange = SPEED_RANGE - punishment.speedPenalty;
        jumpForce = JUMP_FORCE - punishment.jumpPenalty;
        health -= punishment.healthPenalty;
        punishmentCooldown = PUNISHMENT_COOLDOWN;
    }

    private void HandleTimers()
    {
        if(punishmentCooldown > 0)
        {
            punishmentCooldown -= Time.deltaTime;
        }
        if (health <= 0)
            Destroy(this);
        else
        {
            health -= Time.deltaTime;
        }
    }

    private void HandleShooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {

        Instantiate(bullet, bulletSpawn.position, transform.rotation);

        Debug.Log("Projectile Sent");
    }

    public int GetHealth()
    {
        return (int)health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        gameManager.MainMenu();
    }
}
