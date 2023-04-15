using UnityEngine;

public abstract class DataListView<T> : ListView where T : Data
{
    protected DataListItemView<T> prefab;

    public void AddDataItem(T data)
    {
        var itemView = Instantiate(prefab, scrollRect.content.transform)
            .GetComponent<DataListItemView<T>>();
        itemView.SetData(data);

        AddItem(itemView);
    }

    public void RemoveDataItem(T data)
    {
        foreach (var itemView in itemViews)
        {
            if (itemView is not DataListItemView<T> dataListItemView) continue;
            if (data != dataListItemView.Data) continue;
            RemoveItem(itemView);
            return;
        }
    }

    public int FindItemIndex(T data)
    {
        return itemViews.FindIndex(view =>
        {
            if (view is DataListItemView<T> dataListItemView)
            {
                return dataListItemView.Data == data;
            }

            return false;
        });
    }

    public void MoveItemUp(T data)
    {
        var index = FindItemIndex(data);
        if (index < 1) return;

        (itemViews[index], itemViews[index - 1]) = (itemViews[index - 1], itemViews[index]);
        UpdateAllItem();
    }

    public void MoveItemDown(T data)
    {
        var index = FindItemIndex(data);
        if (index < 0 || index >= itemViews.Count - 1) return;

        (itemViews[index], itemViews[index + 1]) = (itemViews[index + 1], itemViews[index]);
        UpdateAllItem();
    }
}