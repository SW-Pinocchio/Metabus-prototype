using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //asd
    [Tooltip("오브젝트 이름")]
    public string objName;
    bool objFocused;
    bool objPressed;

    void Awake()
    {
        objFocused = false;
        objPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        objFocused = false;
        objPressed = false;
    }

    public void ChangeColor()
    {
        if (objFocused) // raycast에 의해 오브젝트가 포커싱 될 때: red
            GetComponent<MeshRenderer>().material.color = Color.red;
        else if (objPressed) // raycast에 의해 오브젝트가 클릭될 때: blue
            GetComponent<MeshRenderer>().material.color = Color.blue;
        else // 오브젝트의 기본색: white
            GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public bool ObjFocused
    {
        get { return objFocused; }
        set { objFocused = value; }
    }

    public bool ObjPressed
    {
        get { return objPressed; }
        set { objPressed = value; }
    }
}
