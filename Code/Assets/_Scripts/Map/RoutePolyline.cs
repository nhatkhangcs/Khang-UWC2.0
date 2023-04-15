using System;
using System.Collections.Generic;
using Mapbox.Utils;
using Shapes;
using UnityEngine;

public class RoutePolyline : MultipleCoordinatesMapEntity
{
    [SerializeField] private Polyline polyline;

    private void Start()
    {
        MapWrapper.Instance.MapUpdated += MapUpdatedHandler;
    }

    public override void UpdateCoordinates(List<Vector2d> coordinates)
    {
        this.coordinates = coordinates;
        MapUpdatedHandler();
    }

    public void UpdateCoordinates(List<Coordinate> coordinates)
    {
        List<Vector2d> rawCoordinates = new();

        foreach (var coordinate in coordinates)
        {
            rawCoordinates.Add(new Vector2d(coordinate.Latitude, coordinate.Longitude));
        }

        UpdateCoordinates(rawCoordinates);
    }

    protected override void MapUpdatedHandler()
    {
        List<Vector2> newPoints = new List<Vector2>();
        foreach (var coordinate in coordinates)
        {
            newPoints.Add(MapManager.Instance.GeoToWorldPosition(coordinate));
        }

        polyline.SetPoints(newPoints);
    }
}