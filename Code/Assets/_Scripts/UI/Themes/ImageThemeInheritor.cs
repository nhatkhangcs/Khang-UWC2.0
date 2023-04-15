using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageThemeInheritor : MonoBehaviour
{
    [SerializeField] private ColorThemeInheritorType colorThemeInheritorType;

    private void Start()
    {
        switch (colorThemeInheritorType)
        {
            case ColorThemeInheritorType.PrimaryColor:
                GetComponent<Image>().color = VisualManager.Instance.PrimaryColor;
                break;
            case ColorThemeInheritorType.SecondaryColor:
                GetComponent<Image>().color = VisualManager.Instance.SecondaryColor;
                break;
            case ColorThemeInheritorType.SubtleGreyColor:
                GetComponent<Image>().color = VisualManager.Instance.SubtleGreyColor;
                break;
            case ColorThemeInheritorType.StaffEntityColor:
                GetComponent<Image>().color = VisualManager.Instance.StaffEntityColor;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}