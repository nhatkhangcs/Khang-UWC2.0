using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public abstract class ListView : MonoBehaviour, IShowHideAnimatable
{
    protected ScrollRect scrollRect;
    protected List<ListItemView> itemViews = new();

    protected static float VERTICAL_SPACING = 10f;

    protected RectTransform rectTransform;

    protected virtual void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        rectTransform = GetComponent<RectTransform>();
        ApplicationManager.Instance.AddInitWork(Init, ApplicationManager.InitState.UI);
    }

    protected virtual void Init()
    {
    }

    public virtual void AddItem(ListItemView itemView)
    {
        itemViews.Add(itemView);
        UpdateItem(itemView);
    }

    public virtual void UpdateItem(ListItemView itemView)
    {
        float yPos = 0;
        var index = itemViews.FindIndex(view => view == itemView);
        if (itemViews.Count != 0) yPos = -index * (VERTICAL_SPACING + itemView.Height);
        itemView.SetParent(scrollRect.content);
        itemView.SetPosition(new Vector2(0, yPos));
        UpdateScrollRect();
    }

    public void UpdateAllItem()
    {
        foreach (var itemView in itemViews)
        {
            UpdateItem(itemView);
        }
    }

    public virtual void RemoveItem(ListItemView itemView)
    {
        var index = itemViews.FindIndex(view => view == itemView);
        if (index == -1) throw new Exception();
        itemViews.RemoveAt(index);
        Destroy(itemView.gameObject);

        for (int i = index; i < itemViews.Count; i++)
        {
            UpdateItem(itemViews[i]);
        }
    }

    public void RemoveAllItem()
    {
        foreach (var view in itemViews)
        {
            Destroy(view.gameObject);
        }

        itemViews = new();
        UpdateScrollRect();
    }
    
    protected virtual void UpdateScrollRect()
    {
        var totalHeight = itemViews.Sum(i => i.Height);
        var newScrollRectSize = new Vector2(0, totalHeight + (itemViews.Count - 1) * VERTICAL_SPACING);
        scrollRect.content.sizeDelta = newScrollRectSize;
    }

    protected void RemoveAllItemViews()
    {
        foreach (var itemView in itemViews)
        {
            Destroy(itemView.gameObject);
        }

        itemViews = new();
    }

    public virtual Task AnimateShow()
    {
        return rectTransform.DOAnchorPosX(0, VisualManager.Instance.ListAndPanelTime)
            .SetEase(Ease.OutCubic)
            .AsyncWaitForCompletion();
    }

    public virtual Task AnimateHide()
    {
        return rectTransform.DOAnchorPosX(-1000, VisualManager.Instance.ListAndPanelTime)
            .SetEase(Ease.InCubic)
            .AsyncWaitForCompletion();
    }
}