public class VehicleDataListItemView : DataListItemView<VehicleData>
{
    public override void SetData(VehicleData data)
    {
        base.SetData(data);

        PrimaryText = data.ID;
        SecondaryText = data.Category;

        UpdateView();
        
        button.onClick.AddListener(() => VehicleInformationPanel.Instance.Show(data));
    }
    
    protected override void UpdateView()
    {
        primaryText_TMP.text = PrimaryText;
        secondaryText_TMP.text = SecondaryText;
    }
}