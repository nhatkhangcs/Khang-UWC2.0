public class StaffDataListItemView : DataListItemView<StaffData>
{
    public override void SetData(StaffData data)
    {
        base.SetData(data);

        PrimaryText = data.Name;
        SecondaryText = data.Role + " - " +
                        LanguageTranslation.GetText(LanguageTranslation.TextType.Staff_Currently_At,
                            LanguageTranslation.ReturnTextOption.lower_case) + " " + data.HomeAddress;

        UpdateView();

        button.onClick.AddListener(() => StaffInformationPanel.Instance.Show(data));
    }

    protected override void UpdateView()
    {
        primaryText_TMP.text = PrimaryText;
        secondaryText_TMP.text = SecondaryText;
    }
}