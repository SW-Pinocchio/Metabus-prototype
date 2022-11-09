using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    float maxDistance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Object");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance ,layerMask))
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
