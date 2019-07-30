#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [Header("Attributes")]

    [SerializeField]
    private float TIME_BETWEEN_DISABLES;

    [SerializeField]
    private float TIME_OF_DISABLES;
    
    private float timerBTW;
    
    private float timerDuring;

    [HideInInspector]
    public string key = "None";

    [HideInInspector]
    public bool inGame = false;

    [Space]

    [Header("References")]

    public KeyDisabled keyDisabled;

    public PlayerController playerController;

    private void Start()
    {
        timerBTW = TIME_BETWEEN_DISABLES;
    }

    private void Update()
    {
        if (inGame)
        {
            if (timerBTW < 0)
            {
                key = keyDisabled.ChooseKey();
                timerBTW = TIME_BETWEEN_DISABLES + TIME_OF_DISABLES;
                timerDuring = TIME_OF_DISABLES;
                Debug.Log("key = " + key);
                if (key == "jump")
                {
                    playerController.notJump = true;
                    Debug.Log("Don't Jump");
                }
                else if (key == "moveLeft")
                {
                    playerController.notMoveLeft = true;
                    Debug.Log("Don't Move Left");
                }
                else if (key == "moveRight")
                {
                    playerController.notMoveRight = true;
                    Debug.Log("Don't Move Right");
                }
            }
            else
            {
                timerBTW -= Time.deltaTime;
            }
            if (timerDuring <= 0)
            {
                key = "None";
                playerController.notJump = false;
                playerController.notMoveLeft = false;
                playerController.notMoveRight = false;
            }
            else
            {
                timerDuring -= Time.deltaTime;
            }
        }
    }

}
