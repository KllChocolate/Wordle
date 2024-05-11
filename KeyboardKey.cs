using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
    [Header("Elemnts")]
    public Image background;
    public TextMeshProUGUI letterText;

    [Header("Events")]
    public static Action<char> onKeyPressed;

    private void Awake()
    {
        background = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
    }

    private void SendKeyPressedEvent()
    {
        onKeyPressed?.Invoke(letterText.text[0]);
    }
    public char GetLetter()
    {
        return letterText.text[0];
    }
    public void SetValid() 
    {
        background.color = new Color (0.3137255f, 0.5490196f, 0.2941177f, 1f);
    }
    public void SetPotential()
    {
        background.color = new Color(0.7058824f, 0.6156863f, 0.227451f, 1f);
    }
    public void SetInvalid()
    {
        background.color = new Color(0.2156863f, 0.2156863f, 0.2352941f, 1f); 
    }
}
