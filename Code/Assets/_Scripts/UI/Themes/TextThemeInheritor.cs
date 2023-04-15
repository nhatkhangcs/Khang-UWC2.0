using System;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TMP_Text))]
public class TextThemeInheritor : MonoBehaviour
{
    [SerializeField] private TextThemeInheritorType textThemeInheritorType;

    private void Start()
    {
        switch (textThemeInheritorType)
        {
            case TextThemeInheritorType.PrimaryTextColor:
                GetComponent<TMP_Text>().color = VisualManager.Instance.PrimaryTextColor;
                break;
            case TextThemeInheritorType.SecondaryTextColor:
                GetComponent<TMP_Text>().color = VisualManager.Instance.SecondaryTextColor;
                break;
            case TextThemeInheritorType.WhiteTextColor:
                GetComponent<TMP_Text>().color = VisualManager.Instance.WhiteTextColor;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}