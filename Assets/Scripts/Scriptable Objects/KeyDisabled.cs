using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Keys Disabled", menuName = "Settings/Keys Disabled")]
public class KeyDisabled : ScriptableObject
{
    public string[] keysDisabled;

    public string ChooseKey()
    {
        int key = Random.Range(0, keysDisabled.Length - 1);

        return keysDisabled[key];
    }
}
