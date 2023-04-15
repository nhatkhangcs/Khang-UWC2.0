using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public abstract class DataListItemView<T> : ListItemView where T : Data
{
    [SerializeField] protected Image image;
    [SerializeField] protected TMP_Text primaryText_TMP;
    [SerializeField] protected TMP_Text secondaryText_TMP;
    [SerializeField] protected Button button;

    public T Data { get; protected set; }

    public string PrimaryText;
    public string SecondaryText;

    public virtual void SetData(T data)
    {
        Data = data;
    }

    protected abstract void UpdateView();

    public void Choose()
    {
        button.OnPointerClick(new PointerEventData(EventSystem.current));
    }
}