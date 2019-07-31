using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Punishment
{
    public float speedPenalty;
    public float jumpPenalty;
    public float healthPenalty;
}

public class Punishments : MonoBehaviour
{
    
    public Punishment[] punishments;
    private int punishmentIndex = -1;

    public Punishment GivePunishment()
    {
        if(punishmentIndex < punishments.Length - 2)
        {
            punishmentIndex++;
            return punishments[punishmentIndex];
        }
        else
        {
            return punishments[punishmentIndex];
        }
    }
}
