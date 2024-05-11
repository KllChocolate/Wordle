using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;
    [Header("Elements")]
    [SerializeField] private string secretWord;
    [SerializeField] private TextMeshProUGUI answer;
    [SerializeField] private TextMeshProUGUI answer2;

    private string[] solutions;

    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this; 
        }
        else 
        { 
            Destroy(gameObject); 
        }
    }
    private void Start()
    {
        LoadData();
        SetRandomWord();
    }

    public void SetRandomWord()
    {
        secretWord = solutions[Random.Range(0, solutions.Length)];
        secretWord = secretWord.ToUpper().Trim();
        answer.text = secretWord;
        answer2.text = secretWord;
    }
    public void LoadData()
    {
        TextAsset textFile = Resources.Load("official_wordle_common") as TextAsset;
        solutions = textFile.text.Split('\n');

    }
    public string GetSecreWord() 
    { 
        return secretWord.ToUpper(); 
    }

}
