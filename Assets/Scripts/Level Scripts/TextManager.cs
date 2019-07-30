using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    [Header("References")]

    public PlayerController player;

    public KeyManager keyManager;

    public TextMeshProUGUI keyText;
    
    public TextMeshProUGUI timerText;

    public bool inGame = false;

    private void Update()
    {
        if (inGame)
        {
            keyText.text = "Key Disabled: " + keyManager.key;
            timerText.text = player.GetHealth().ToString();
        }
    }
}
