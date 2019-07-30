using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Settings/Keybindings")]
public class Keybindings : ScriptableObject
{
    [Header("Player Movement")]
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;

    [Space]
    [Header("Menu Options")]
    public KeyCode pause;

    public KeyCode GetKey(string key)
    {
        switch (key)
        {
            case "moveLeft":
                return moveLeft;

            case "moveRight":
                return moveRight;

            case "jump":
                return jump;
                
            case "pause":
                return pause;
            
            default:
                return KeyCode.None;
        }
    }
}
