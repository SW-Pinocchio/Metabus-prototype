using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    //asd
    Ray ray;
    RaycastHit hit;
    float maxDistance = 10f;
    int objLayerMask;

    void Awake()
    {
        objLayerMask = 1 << LayerMask.NameToLayer("Object");
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance , objLayerMask))
        {
            if (Input.GetMouseButton(0))
            {
                hit.collider.gameObject.GetComponent<ObjectController>().ObjPressed = true;
            }
            else
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<ObjectController>().ObjFocused = true;
            }
        }
    }
}
