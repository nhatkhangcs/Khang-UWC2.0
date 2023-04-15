public class VehicleDataListView : DataListView<VehicleData>
{
    protected override void Init()
    {
        base.Init();

        prefab = ResourceManager.Instance.VehicleDataListItemView;

        var allVehicles = DatabaseManager.Instance.AllVehicles;
        foreach (var vehicleData in allVehicles)
        {
            AddDataItem(vehicleData);
        }
    }
}