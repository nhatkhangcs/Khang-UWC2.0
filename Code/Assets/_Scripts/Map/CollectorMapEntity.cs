using System;
using System.Collections;
using Mapbox.Utils;
using UnityEngine;

public class CollectorMapEntity : SingleCoordinateMapEntity<StaffData>
{
    private Vector3 lastTransformPos;
    private Vector2d lastCoordinate;

    public void Init(StaffData data)
    {
        AssignData(data);

        button.onClick.AddListener(() => { MapManager.Instance.ViewingCollectorID = data.ID; });

        StartCoroutine(UpdatePosition_CO());
    }

    public override void ValueChangedHandler()
    {
    }

    IEnumerator UpdatePosition_CO()
    {
        while (true)
        {
            BackendCommunicator.Instance.MapAPICommunicator.GetCollectorPosition(data.ID,
                (isSucceed, routeTraversedData) =>
                {
                    if (isSucceed)
                    {
                        var currentCoordinate = new Vector2d(routeTraversedData.CurrentPos.Latitude,
                            routeTraversedData.CurrentPos.Longitude);
                        UpdateCoordinate(currentCoordinate);

                        transform.rotation = Quaternion.Euler(0, 0,
                            (float)Vector2d.Angle(Vector2d.up, currentCoordinate - lastCoordinate));
                        lastCoordinate = currentCoordinate;
                        transform.SetSiblingIndex(transform.parent.childCount - 1);
                    }
                });

            yield return new WaitForSeconds(10f);
        }
    }
}