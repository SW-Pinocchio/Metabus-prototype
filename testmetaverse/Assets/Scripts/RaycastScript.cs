using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    RaycastHit hit;
    float MaxDistance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.blue, 0.3f);
            if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance))
            {
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        
    }
}
