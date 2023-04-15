using Mapbox.Utils;
using UnityEngine;

public abstract class MapPopup<T> : MapEntity where T : Data
{
    private const float VERTICAL_POPUP_OFFSET = 0.3f;

    protected Vector2d coordinate;
    protected override void MapUpdatedHandler()
    {
        transform.position = MapManager.Instance.GeoToWorldPosition(coordinate) +
                             Vector2.up * VERTICAL_POPUP_OFFSET;
    }

    public abstract void Show(T data, Vector2d coordinate);
    public abstract void Hide();
}