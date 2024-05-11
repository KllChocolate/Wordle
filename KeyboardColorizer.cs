using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    [Header("Elements")]
    public KeyboardKey[] keys;
    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }
    public void Colorize(string secretWord, string wordToCheck)
    {
        for (int i = 0; i < keys.Length; i++) 
        {
            char keyLetter = keys[i].GetLetter();
            
            for(int j = 0; j < wordToCheck.Length; j++) 
            { 
                if (keyLetter != wordToCheck[j]) 
                { 
                    continue;
                }

                if (keyLetter == secretWord[j])
                {
                    keys[i].SetValid();
                }
                else if (secretWord.Contains(keyLetter))
                {
                    keys[i].SetPotential();
                }
                else 
                {
                    keys[i].SetInvalid();
                }
            }
        }
    }
}
