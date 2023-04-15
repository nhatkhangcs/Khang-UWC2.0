using System.Collections.Generic;
using Mapbox.Utils;
using UnityEngine;


public abstract class MultipleCoordinatesMapEntity : MapEntity
{
    [SerializeField] protected List<Vector2d> coordinates = new();

    public abstract void UpdateCoordinates(List<Vector2d> coordinates);
}