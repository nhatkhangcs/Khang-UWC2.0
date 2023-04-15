using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingListItemView : ListItemView, ITMPDeferrable
{
    private const float HORIZONTAL_OPTIONS_SPACING = 10f;
    private const float EXTRA_HORIZONTAL_TEXT_MARGIN = 10f;
    private const float EXTRA_HORIZONTAL_OPTIONS_MARGIN = 25f;

    private Dictionary<Enum, Button> buttons = new();

    private Func<Enum> getProperty;
    private Action<Enum> setProperty;
    private Type enumType;

    [SerializeField] private TMP_Text optionText;

    public void Init(string optionName, Func<Enum> getProperty, Action<Enum> setProperty, Type e)
    {
        optionText.text = optionName;
        optionText.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(EXTRA_HORIZONTAL_TEXT_MARGIN, 0f);

        this.getProperty = getProperty;
        this.setProperty = setProperty;
        enumType = e;

        foreach (var option in Enum.GetValues(e))
        {
            var button = Instantiate(ResourceManager.Instance.SettingOptionButton, transform)
                .GetComponent<Button>();

            var buttonTMP_Text = button.GetComponentInChildren<TMP_Text>();
            buttonTMP_Text.text = option.ToString();
            buttonTMP_Text.ForceMeshUpdate();

            button.onClick.AddListener(() => ButtonClickHandler((Enum)option));

            buttons.Add((Enum)option, button);
        }

        ButtonClickHandler(GetProperty());
    }

    public Enum GetProperty()
    {
        return getProperty.Invoke();
    }

    public void SetProperty(Enum e)
    {
        setProperty.Invoke(e);
    }

    private void ButtonClickHandler(Enum option)
    {
        foreach (var button in buttons)
        {
            button.Value.GetComponentInChildren<TMP_Text>().color =
                VisualManager.Instance.SecondaryTextColor;
        }

        buttons[option].GetComponentInChildren<TMP_Text>().color = VisualManager.Instance.PrimaryColor;
        SetProperty(option);
    }

    public void ExecuteDeferredWork()
    {
        float widthSoFar = EXTRA_HORIZONTAL_OPTIONS_MARGIN;
        var enumValues = Enum.GetValues(enumType);
        for (int i = enumValues.Length - 1; i >= 0; i--)
        {
            var buttonRectTransform =
                buttons[(Enum)enumValues.GetValue(i)].GetComponent<RectTransform>();
            var buttonTMP_Text =
                buttons[(Enum)enumValues.GetValue(i)].GetComponentInChildren<TMP_Text>();

            var textBoundsWidth = buttonTMP_Text.textBounds.size.x;
            buttonRectTransform.sizeDelta = new Vector2(textBoundsWidth, 50);
            buttonRectTransform.anchoredPosition = new Vector2(-widthSoFar, 0);

            widthSoFar += textBoundsWidth + HORIZONTAL_OPTIONS_SPACING;
        }
    }
}