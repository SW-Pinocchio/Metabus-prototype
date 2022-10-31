using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<MeshRenderer>().material.color == Color.red)
        {
            Invoke("Cchange", 1);
            
        }
        
    }

    void Cchange()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
