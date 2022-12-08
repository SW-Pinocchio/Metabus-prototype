using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLogic
{
    public static readonly Vector3 grid = new Vector3(0.2f, 0.1f, 0.2f);
    public static int itemLayerMask = LayerMask.GetMask("Object");

    public static Vector3 SnapToGrid(Vector3 input)
    {
        return new Vector3(Mathf.Round(input.x / grid.x) * grid.x,
            Mathf.Round(input.y / grid.y) * grid.y,
            Mathf.Round(input.z / grid.z) * grid.z);
    }
}
