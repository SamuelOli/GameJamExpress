using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectSO.Dir dir, PlacedObjectSO placedObjectSO)
    {
        Transform objectTransform = Instantiate(placedObjectSO.prefab, worldPosition, Quaternion.Euler(0, 0, placedObjectSO.GetRotationAngle(dir)));

        PlacedObject placedObject = objectTransform.GetComponent<PlacedObject>();

        placedObject.placedObjectSO = placedObjectSO;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }

    private PlacedObjectSO placedObjectSO;
    private Vector2Int origin;
    private PlacedObjectSO.Dir dir;

    public List<Vector2Int> GetGridPositionList()
    {
        return placedObjectSO.GetGridPositionList(origin, dir);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
