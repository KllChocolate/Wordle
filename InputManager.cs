using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager: MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;

    [Header("Settings")]
    private int currentWordContainerIndex;
    private bool canAddLeeter = true;

    public int numberOfGuesses = 0;
    private string[] validWords;
    public GameObject invalidWordText;
    public GameObject ending;
    public TextMeshProUGUI number;

    void Start()
    {
        Initialize();
        KeyboardKey.onKeyPressed += KeyPressedCallback;

    }
    public void Initialize()
    {
        for (int i = 0; i < wordContainers.Length; i++)
            wordContainers[i].Initialize();

        TextAsset textFile = Resources.Load("official_wordle_all") as TextAsset;
        validWords = textFile.text.ToUpper().Split('\n');
    }
    private void KeyPressedCallback(char letter)
    {
        if (!canAddLeeter) 
            return;
        wordContainers[currentWordContainerIndex].Add(letter);
        
        if (wordContainers[currentWordContainerIndex].IsComplete())
        {
            canAddLeeter = false;
            EnableTryButton();
        }
    }
    public void CheckWord()
    {
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSecreWord();
        
        numberOfGuesses++;

        if (!InValidWord(wordToCheck))
        {
            invalidWordText.gameObject.SetActive(true);
            return;
        }

        wordContainers[currentWordContainerIndex].Colorize(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);
        number.text = numberOfGuesses.ToString();

        if (wordToCheck == secretWord) 
        {
            ending.gameObject.SetActive(true);
        }
        else if (currentWordContainerIndex >= 5)
        {
            ending.gameObject.SetActive(true);
            canAddLeeter = false;
        }
        else 
        {
            canAddLeeter = true;
            DisableTryButton();
            currentWordContainerIndex++; 
        }
    }
    public void BackspacePressedCallback()
    {
        bool removedLetter = wordContainers[currentWordContainerIndex].RemoveLetter();
        if (removedLetter) DisableTryButton();

            canAddLeeter = true ;
            invalidWordText.gameObject.SetActive(false);
    }

    private void EnableTryButton()
    { 
        tryButton.interactable = true; 
    }
    private void DisableTryButton()
    { 
        tryButton.interactable = false;
    }
    public bool InValidWord(string word)
    {
        for (int i = 0; i < validWords.Length; i++)
        {
            if (validWords[i] == word)
            {
                return true;
                
            }
        }
        return false;
    }

    public void NewGame()
    {
        ClearBoard();
        ClearKey();
        numberOfGuesses = 0;
        canAddLeeter = true;
        ending.gameObject.SetActive(false);
        WordManager.instance.SetRandomWord();
    }

    private void ClearBoard()
    {
        for (int row = 0; row < wordContainers.Length; row++)
        {
            for (int col = 0; col < wordContainers[row].letterContainers.Length; col++)
            {
                wordContainers[row].letterContainers[col].SetLetter('\0');
                wordContainers[row].letterContainers[col].letterContainer.color = Color.black;
                wordContainers[row].currentLetterIndex = 0;
                currentWordContainerIndex = 0;
            }
        }
    }
    private void ClearKey()
    {

        for (int i = 0; i < keyboardColorizer.keys.Length; i++)
        {
            keyboardColorizer.keys[i].background.color = new Color(0.5058824f, 0.5137255f, 0.5176471f, 1f);
        }

    }
    public void Exit()
    {
        Application.Quit();
    }

}
