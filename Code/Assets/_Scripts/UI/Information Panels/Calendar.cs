using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    [SerializeField] private TMP_Text currentMonthAndYear;
    [SerializeField] private Button prevMonthButton;
    [SerializeField] private Button nextMonthButton;
    [SerializeField] private List<Button> dateButtons;
    private List<TMP_Text> dateTexts = new();

    private DateTime currentMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);

    private Action<DateTime> assignedAction;
    private DateTime choosingDate;
    
    private void Awake()
    {
        for (int i = 0; i < 35; i++)
        {
            dateTexts.Add(dateButtons[i].GetComponentInChildren<TMP_Text>());
        }
        
        for (int i = 0; i < dateButtons.Count; i++)
        {
            var assignedDate = currentMonth.AddDays(i);
            dateButtons[i].onClick.AddListener(() =>
            {
                choosingDate = assignedDate; 
                ConfirmAction();
            });
        }
        
        prevMonthButton.onClick.AddListener(() => SwitchMonth(-1));
        nextMonthButton.onClick.AddListener(() => SwitchMonth(1));

        SwitchMonth(0);
    }

    private void SwitchMonth(int monthOffset)
    {
        currentMonth = currentMonth.AddMonths(monthOffset);
        currentMonthAndYear.text = currentMonth.ToString("MM, yyyy");

        UpdateCalendar();
    }

    private void UpdateCalendar()
    {
        DateTime startDate = currentMonth;
        while (startDate.Date.DayOfWeek != DayOfWeek.Monday)
        {
            startDate = startDate.AddDays(-1);
        }

        for (int i = 0; i < 35; i++)
        {
            dateTexts[i].text = startDate.Day.ToString();
            startDate = startDate.AddDays(1);
        }
    }

    public void AssignAction(Action<DateTime> action)
    {
        assignedAction = action;
    }

    public void ConfirmAction()
    {
        assignedAction?.Invoke(choosingDate);
    }
}