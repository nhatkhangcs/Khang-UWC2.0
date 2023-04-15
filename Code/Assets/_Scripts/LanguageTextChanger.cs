using System;
using TMPro;
using UnityEngine;

public class LanguageTextChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private LanguageTranslation.TextType type;
    [SerializeField] private LanguageTranslation.ReturnTextOption option;
    
    private void Start()
    {
        text.text = LanguageTranslation.GetText(type, option);
    }
}