using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterContainer : MonoBehaviour
{
    [Header("Elements")]
    public Image letterContainer;
    public TextMeshProUGUI letter;

    private void Awake()
    {
        letterContainer = GetComponentInChildren<Image>();
    }
    public void Initialize()
    {
        letter.text = "";
        letterContainer.color = Color.black;
    }

    public void SetLetter(char letter)
    {
        this.letter.text = letter.ToString();
    }
    public void SetValid()
    { letterContainer.color = new Color (0.3137255f, 0.5490196f, 0.2941177f, 1f); }
    public void SetPotential()
    { letterContainer.color = new Color(0.7058824f, 0.6156863f, 0.227451f, 1f); }
    public void SetInvalid()
    { letterContainer.color = new Color(0.2156863f, 0.2156863f, 0.2352941f, 1f); }

    public char GetLetter() 
    {
        return letter.text[0];
    }

}
