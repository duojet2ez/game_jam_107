using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelManager : MonoBehaviour
{
    [Header("Attributes")]

    public float checkRadius;

    [Space]

    [Header("References")]

    public GameManager gameManager;

    public LayerMask whatIsPlayer;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        bool playerNear = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        if (playerNear)
        {
            gameManager.LoadNextLevel();
        }

    }
}
