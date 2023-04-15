using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Mapbox.Utils;
using Shapes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MCPMapEntity : SingleCoordinateMapEntity<MCPData>
{
    #region Toggle

    public static event Action<MCPMapEntity, bool> ToggleStateChanged;

    private static bool groupingSelect;

    public static bool GroupingSelect
    {
        get => groupingSelect;
        set
        {
            groupingSelect = value;
            if (groupingSelect)
            {
                foreach (var (key, _) in ToggleStates)
                {
                    key.ChangeToSelectMode();
                }
                
            }
            else
            {
                foreach (var (key, _) in ToggleStates)
                {
                    key.ChangeToInfoMode();
                    key.HideDisc();
                }
            }

            ChosenEntities = new();
        }
    }

    public static Dictionary<MCPMapEntity, bool> ToggleStates = new();
    public static List<MCPMapEntity> ChosenEntities = new();

    #endregion

    [SerializeField] private Image background;
    [SerializeField] private Disc choiceDisc;
    [SerializeField] private TMP_Text chosenOrderText;


    public void Init(MCPData data)
    {
        ToggleStates.Add(this, false);

        AssignData(data);
        UpdateCoordinate(new Vector2d(data.Latitude, data.Longitude));

        ToggleStateChanged += (_, _) =>
        {
            var index = ChosenEntities.FindIndex(entity => entity == this);
            if (index == -1)
            {
                chosenOrderText.text = "";
            }
            else
            {
                chosenOrderText.text = (index + 1).ToString();
            }
        };

        ChangeToInfoMode();
        HideDisc();
    }

    public override void ValueChangedHandler()
    {
        var percentage = data.StatusPercentage;

        background.color = choiceDisc.Color =
            chosenOrderText.color = VisualManager.Instance.GetMCPColor(percentage / 100f);
    }

    private void ChangeToInfoMode()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            PrimarySidebar.Instance.OnViewChanged(ViewType.MCPsOverview);
            MCPInformationPanel.Instance.Show(data);
            ToggleStates[this] = false;
            MapManager.Instance.HideRoutePolyline();
        });
    }

    private void ChangeToSelectMode()
    {
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() =>
        {
            if (GroupingSelect)
            {
                ToggleStates[this] = !ToggleStates[this];

                if (ToggleStates[this])
                {
                    ShowDisc();
                    ChosenEntities.Add(this);
                }
                else
                {
                    HideDisc();
                    ChosenEntities.Remove(this);
                }

                ToggleStateChanged?.Invoke(this, ToggleStates[this]);
            }
            else
            {
                choiceDisc.gameObject.SetActive(ToggleStates[this]);
            }

            List<Vector2d> vector2ds = new();
            foreach (var entity in ChosenEntities)
            {
                vector2ds.Add(entity.coordinate);
            }

            BackendCommunicator.Instance.MapAPICommunicator.GetCollectorPosition(
                StaffInformationPanel.Instance.Data.ID, (isSucceeded, routeTraversedData) =>
                {
                    if (!isSucceeded) return;

                    vector2ds.Add(new Vector2d(routeTraversedData.CurrentPos.Latitude,
                        routeTraversedData.CurrentPos.Longitude));
                    MapManager.Instance.GetRoute(vector2ds, (isSucceeded, list) =>
                    {
                        if (!isSucceeded) return;

                        List<Coordinate> coordinates = new();

                        foreach (var vector2d in list)
                        {
                            coordinates.Add(new Coordinate(vector2d.x, vector2d.y));
                        }

                        MapManager.Instance.ShowRoutePolyline(coordinates);
                    });
                });
        });
    }

    private void ShowDisc()
    {
        choiceDisc.gameObject.SetActive(true);
        choiceDisc.transform.DORotate(new Vector3(0, 0, -90), 45f, RotateMode.WorldAxisAdd)
            .SetSpeedBased(true)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    private void HideDisc()
    {
        if (choiceDisc != null)
        {
            choiceDisc.transform.DOKill();
            choiceDisc.gameObject.SetActive(false);
        }
        if (chosenOrderText != null) chosenOrderText.text = "";
    }

    public void UpdateNumber(List<MCPData> listData)
    {
        var index = listData.FindIndex(data => data == Data);
        if (index == -1)
        {
            chosenOrderText.text = "";
        }
        else
        {
            chosenOrderText.text = (index + 1).ToString();
        }
    }
}