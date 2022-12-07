using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [HideInInspector]
    public BoxCollider boxCollider;
    //[HideInInspector]
    //public LODGroup meshes;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        //meshes = GetComponent<LODGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Material: " + GetComponent<MeshRenderer>().material.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaterial(Material mat)
    {
        //var lods = meshes.GetLODs();
        //for (var i = 0; i < lods.Length; i++)
        //{
        //    for (var j = 0; j < lods[i].renderers.Length; j++)
        //    {
        //        lods[i].renderers[j].material = mat;
        //    }
        //}
        gameObject.GetComponent<Renderer>().material = mat;
    }
}
