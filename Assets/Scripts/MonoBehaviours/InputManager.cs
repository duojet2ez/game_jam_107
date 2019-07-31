using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Keybindings keybindings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public bool GetKeyDown(string key)
    {
        if(Input.GetKeyDown(keybindings.GetKey(key)))
        {
            return true;
        }
        else
        { 
            return false;
        }
    }

    public bool GetKey(string key)
    {
        if (Input.GetKey(keybindings.GetKey(key)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
