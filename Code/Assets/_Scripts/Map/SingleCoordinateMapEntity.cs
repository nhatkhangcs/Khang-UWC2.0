using Mapbox.Utils;
using UnityEngine;
using UnityEngine.UI;

public abstract class SingleCoordinateMapEntity<T> : MapEntity where T : Data
{
    protected T data;
    public T Data => data;
    
    [SerializeField] protected Button button;
    protected Vector2d coordinate;

    public virtual void AssignData(T data)
    {
        this.data = data;
        data.ValueChanged += ValueChangedHandler;

        ValueChangedHandler();
    }

    protected virtual void OnDestroy()
    {
        data.ValueChanged -= ValueChangedHandler;
    }

    protected virtual void UpdateCoordinate(Vector2d coordinate)
    {
        this.coordinate = coordinate;
        MapUpdatedHandler();
    }

    public abstract void ValueChangedHandler();

    protected override void MapUpdatedHandler()
    {
        transform.position = MapManager.Instance.GeoToWorldPosition(coordinate);
    }
}