using System;
using UnityEngine;

public class ListItemView : MonoBehaviour
{
    [SerializeField] protected RectTransform rectTransform;
    public float Height => rectTransform.sizeDelta.y;

    public void SetPosition(Vector2 pos)
    {
        rectTransform.anchoredPosition = pos;
    }

    public void SetParent(RectTransform rectTransform)
    {
        this.rectTransform.SetParent(rectTransform, false);
    }
}