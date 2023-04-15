using UnityEngine;

public class InboxListItemView : DataListItemView<StaffData>
{
    public override void SetData(StaffData data)
    {
        base.SetData(data);
        
        PrimaryText = data.Name;
        SecondaryText = data.Role;

        UpdateView();

        button.onClick.AddListener(() => ListViewManager.Instance.InboxListView.OnInboxChosen(data));
    }

    protected override void UpdateView()
    {
        primaryText_TMP.text = PrimaryText;
        secondaryText_TMP.text = SecondaryText;
    }
}