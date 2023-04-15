using System;

public class InboxListView : DataListView<StaffData>
{
    public event Action<StaffData> InboxChosen;

    protected override void Init()
    {
        base.Init();

        prefab = ResourceManager.Instance.InboxListItemView;

        var allStaffs = DatabaseManager.Instance.AllStaffs;
        foreach (var staff in allStaffs)
        {
            AddDataItem(staff);
        }

        if (itemViews[0] is DataListItemView<StaffData> dataListItemView)
            OnInboxChosen(dataListItemView.Data);
    }

    public void OnInboxChosen(StaffData inbox)
    {
        InboxChosen?.Invoke(inbox);
    }

    public void SelectInboxByStaffId(string staffId)
    {
        foreach (var itemView in itemViews)
        {
            if (itemView is InboxListItemView inboxView)
            {
                if (inboxView.Data.ID == staffId)
                {
                    OnInboxChosen(inboxView.Data);
                    return;
                }
            }
        }
    }
}