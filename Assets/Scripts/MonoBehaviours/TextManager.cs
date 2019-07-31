using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    [Header("Attributes")]

    public bool inGame = false;

    public bool prevInGame = false;

    [Space]

    [Header("References")]

    public PlayerController player;

    public KeyManager keyManager;

    public TextMeshProUGUI keyText;
    
    public TextMeshProUGUI timerText;

    private void Update()
    {  
        if(prevInGame != inGame && inGame == true)
        {
            GetReferences();
            prevInGame = true;
        }
        else if (prevInGame != inGame && inGame == false)
        {
            prevInGame = false;
        }

        if (inGame)
        { 
            keyText.text = "Key Disabled: " + keyManager.key;
            timerText.text = player.GetHealth().ToString();
        }
    }

    public void GetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        keyManager = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyManager>();
    }
}
