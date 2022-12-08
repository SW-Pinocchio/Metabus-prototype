using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPostController : MonoBehaviour
{
    [SerializeField]
    Canvas canvasPost;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        canvasPost.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentMode == GameManager.PlayMode.Build)
        {
            return;
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out var hit, float.MaxValue, ItemLogic.itemLayerMask))
            {
                canvasPost.gameObject.SetActive(true);
            }
        }
        
        if(canvasPost.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            canvasPost.gameObject.SetActive(false);
        }
    }
}
