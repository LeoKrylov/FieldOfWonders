using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameSettings")]
public class SettingsOfGame : ScriptableObject
{

    [SerializeField] private int minWordLength;
    [SerializeField] private int amountOfAttempt;
    public int MinWordLength
    {
        get
        {
            return minWordLength;
        }
    }

    public int AmountOfAttempt
    {
        get
        {
            return amountOfAttempt;
        }
    }

}
