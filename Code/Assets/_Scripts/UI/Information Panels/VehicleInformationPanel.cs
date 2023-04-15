using TMPro;
using UnityEngine;

public class VehicleInformationPanel : InformationPanel<VehicleData>
{
    [SerializeField] private TMP_Text vehicleCategory;
    [SerializeField] private TMP_Text plate;
    [SerializeField] private TMP_Text model;
    [SerializeField] private TMP_Text weight;
    [SerializeField] private TMP_Text capacity;
    [SerializeField] private TMP_Text fuelConsumption;

    protected override void SetData(VehicleData data)
    {
        base.SetData(data);

        vehicleCategory.text = data.Category;
        plate.text = data.ID;
        model.text = LanguageTranslation.GetText(LanguageTranslation.TextType.Vehicle_Model,
            LanguageTranslation.ReturnTextOption.Sentence_case) + ": " + data.Model;
        weight.text = LanguageTranslation.GetText(LanguageTranslation.TextType.Vehicle_Weight,
                          LanguageTranslation.ReturnTextOption.Sentence_case) + ": " +
                      Mathf.CeilToInt(data.Weight) +
                      "kgs";
        capacity.text = LanguageTranslation.GetText(LanguageTranslation.TextType.Vehicle_Capacity,
                            LanguageTranslation.ReturnTextOption.Sentence_case) + ": " +
                        Mathf.CeilToInt(data.Capacity) +
                        "kgs";
        fuelConsumption.text =
            LanguageTranslation.GetText(LanguageTranslation.TextType.Vehicle_Fuel_Consumption,
                LanguageTranslation.ReturnTextOption.Sentence_case) + ": " +
            Mathf.CeilToInt(data.FuelConsumption) + "l/100km";
    }
}