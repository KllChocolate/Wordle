using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Board : MonoBehaviour
{
    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[]
    {
        KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,
        KeyCode.G,KeyCode.H,KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L,
        KeyCode.M,KeyCode.N,KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,
        KeyCode.S,KeyCode.T,KeyCode.U,KeyCode.V,KeyCode.W,KeyCode.X,
        KeyCode.Y,KeyCode.Z,
    };

    public string word;

    private Row[] rows;

    private string[] solutions;
    private string[] validWords;

    private int rowIndex;
    private int columnIndex;

    [Header("States")]
    public Block.State emptyState;
    public Block.State occpiedState;
    public Block.State correctState;
    public Block.State wrongSportState;
    public Block.State incorrectState;

    [Header("UI")]
    //public GameObject tryAgainButton;
    public GameObject newWordButton;
    public GameObject invalidWordText;
    public TextMeshProUGUI answer;



    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
    }
    private void Start()
    {
        LoadData();
        NewGame();
    }
    private void LoadData()
    {
        TextAsset textFile = Resources.Load("official_wordle_common") as TextAsset;
        solutions = textFile.text.Split('\n');

        textFile  = Resources.Load("official_wordle_all") as TextAsset;
        validWords = textFile.text.Split('\n');
        
    }
    public void NewGame()
    {
        ClearBoard();
        SetRandomWord();
        answer.text = word;
        enabled = true;
    }

    public void TryAgain()
    {
        ClearBoard();

        enabled = true;
    }
    private void SetRandomWord()
    { 
        word = solutions[UnityEngine.Random.Range(0, solutions.Length)];
        word = word.ToUpper().Trim();
    }

    private void Update()
    {
        Row currentRow = rows[rowIndex];

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            columnIndex = Mathf.Max(columnIndex - 1, 0);
            currentRow.blocks[columnIndex].SetLetter('\0');
            currentRow.blocks[columnIndex].SetState(emptyState);

            invalidWordText.gameObject.SetActive(false);

        }

        else if (columnIndex >= currentRow.blocks.Length)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SubmitRow(currentRow);
            }
        }
        else
        {
            for (int i = 0; i < SUPPORTED_KEYS.Length; i++)
            {
                if (Input.GetKeyDown(SUPPORTED_KEYS[i]))
                {
                    currentRow.blocks[columnIndex].SetLetter((char)SUPPORTED_KEYS[i]);
                    currentRow.blocks[columnIndex].SetState(occpiedState);
                    columnIndex++;
                    break;
                }
            }
        }
    }
    private void SubmitRow(Row row)
    {
        if(!InValidWord(row.word))
        {
            invalidWordText.gameObject.SetActive(true);
            return;
        }
        string remaining = word;

        for (int i = 0;i < row.blocks.Length; i++) 
        { 
            Block block = row.blocks[i];

            if (block.letter == word[i])
            {
                block.SetState(correctState);
                remaining = remaining.Remove(i, 1);
                remaining = remaining.Insert(i, " ");
            }
            else if (!word.Contains(block.letter))
            {
                block.SetState(incorrectState);
            }
        }
        for (int i = 0;i<row.blocks.Length; i++) 
        { 
            Block block = row.blocks[i];

            if (block.state != correctState && block.state != incorrectState)
            { 
                if(remaining.Contains(block.letter))
                {
                    block.SetState(wrongSportState);

                    int index = remaining.IndexOf(block.letter);
                    remaining = remaining.Remove(index, 1);
                    remaining = remaining.Insert(index, " ");
                }
                else 
                {
                    block.SetState(incorrectState);
                }
            }
        }
        if (HasWon(row))
        {
            enabled = false;
        }


        rowIndex++;
        columnIndex = 0;

        if (rowIndex >= rows.Length) 
        {
            enabled = false;
        }

    }
    private bool InValidWord(string word)
    {
        for (int i = 0; i <validWords.Length; i++) 
        {
            if (validWords[i] == word) 
            {  
                return true; 
            }
        }
        return false;
    }
    private bool HasWon(Row row)
    {
        for (int i = 0; i < row.blocks.Length; i++)
        {
            if (row.blocks[i].state != correctState)
            {
                return false;
            }
        }

        return true;
    }
    private void ClearBoard()
    {
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].blocks.Length; col++)
            {
                rows[row].blocks[col].SetLetter('\0');
                rows[row].blocks[col].SetState(emptyState);
            }
        }

        rowIndex = 0;
        columnIndex = 0;
    }
    private void OnEnable()
    {
        //tryAgainButton.SetActive(false);
        newWordButton.SetActive(false);
    }

    private void OnDisable()
    {
        //tryAgainButton.SetActive(true);
        newWordButton.SetActive(true);
    }
}
