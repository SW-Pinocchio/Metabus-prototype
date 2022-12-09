using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector]
    BoxCollider boxCollider;
    [HideInInspector]
    public LODGroup meshes;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshes = GetComponent<LODGroup>();
    }

    public void SetMaterial(Material mat)
    {
        var lods = meshes.GetLODs();
        for (var i = 0; i < lods.Length; i++)
        {
            for (var j = 0; j < lods[i].renderers.Length; j++)
            {
                lods[i].renderers[j].material = mat;
            }
        }
    }

    public BoxCollider BoxCollider
    {
        get { return boxCollider; }
        set { boxCollider = value; }
    }
}
