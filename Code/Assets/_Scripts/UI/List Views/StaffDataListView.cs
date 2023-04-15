using System;

public class StaffDataListView : DataListView<StaffData>
{
    protected override void Init()
    {
        base.Init();

        prefab = ResourceManager.Instance.StaffDataListItemView;

        var allStaffs = DatabaseManager.Instance.AllStaffs;
        foreach (var staffData in allStaffs)
        {
            AddDataItem(staffData);
        }
    }
    
    public void SelectStaffByStaffId(string staffId)
    {
        foreach (var itemView in itemViews)
        {
            if (itemView is StaffDataListItemView staffView)
            {
               if (staffView.Data.ID == staffId) staffView.Choose();
            }
        }
    }
}