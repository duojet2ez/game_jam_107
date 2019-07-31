using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Attributes")]

    public float speed;

    public float TIME_UNTIL_HIT = 0.1f;

    private float timerUntilHit;

    [Space]

    [Header("References")]

    public Rigidbody2D rb;

    private void Start()
    {
        Debug.Log(transform.position);
        timerUntilHit = TIME_UNTIL_HIT;
        rb.velocity = transform.right * speed * Time.deltaTime;
        DestroyObject(gameObject, 10f);
    }

    private void Update()
    {
        if (timerUntilHit > 0)
            timerUntilHit -= Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (timerUntilHit <= 0)
        {
            EnemyController enemy = hitInfo.GetComponent<EnemyController>();
            PlayerController player = hitInfo.GetComponent<PlayerController>();

            if (enemy != null)
            {
                Debug.Log("HIT ENEMY");
                enemy.TakeDamage(10);
            }
            else if (player != null)
            {
                Debug.Log("HIT PLAYER");
                player.TakeDamage(10);
            }
            Destroy(gameObject);
        }
    }
}
