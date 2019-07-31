using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]

    public float speed;

    public float checkRadius;

    public float health = 100f;

    public float TIME_BTW_BULLETS;

    private float timerBTWBullets;

    private bool facesRight = true;
    [Space]

    [Header("References")]

    public Transform player;

    public Transform groundCheck;

    public Transform blockCheck;

    public Transform bulletSpawn;

    public LayerMask whatIsGround;

    public Rigidbody2D rb;

    public GameObject bullet;

    private void Update()
    {
        float distance = transform.position.x - player.position.x;

        if(Mathf.Abs(distance) > checkRadius)
        {
            Patrol();
        }
        else {
            FollowPlayer(distance);
        }

        if (timerBTWBullets <= 0)
        {
            Shoot();
            timerBTWBullets = TIME_BTW_BULLETS;
        }
        else
        {
            timerBTWBullets -= Time.deltaTime;
        }
    }

    private void Patrol()
    {
        bool isGrounded = Physics2D.Linecast(groundCheck.position, groundCheck.position + Vector3.down, whatIsGround);
        bool isBlocking = Physics2D.Linecast(blockCheck.position, blockCheck.position + transform.right * 0.2f, whatIsGround);
        Debug.DrawLine(groundCheck.position, groundCheck.position + Vector3.down);
        if (!isGrounded || isBlocking)
        {
            facesRight = !facesRight;

            Vector3 rotation = new Vector3(0, 180, 0);

            transform.Rotate(rotation);
        }

        float moveX;

        if (facesRight)
        {
            moveX = speed;
        }
        else moveX = -speed;

        rb.velocity = new Vector2(moveX, 0);
    }

    private void FollowPlayer(float distance)
    {

        bool isGrounded = Physics2D.Linecast(groundCheck.position, groundCheck.position + Vector3.down, whatIsGround);

        if (facesRight && distance > 0 || !facesRight && distance < 0)
        {
            facesRight = !facesRight;

            Vector3 rotation = new Vector3(0, 180, 0);

            transform.Rotate(rotation);
        }
        if (isGrounded && Mathf.Abs(distance) > 2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, player.position.y), speed * Time.deltaTime);
        }
    }

    public void Shoot()
    {

        Instantiate(bullet, bulletSpawn.position, transform.rotation);
        
        Debug.Log("Projectile Sent");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
