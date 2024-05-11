using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public Block[] blocks {  get; private set; }

    public string word
    {
        get
        {
            string word = "";

            for (int i = 0; i < blocks.Length; i++)
            {
                word += blocks[i].letter;
            }

            return word;
        }
    }
    private void Awake()
    {
        blocks = GetComponentsInChildren<Block>();
    }
}
