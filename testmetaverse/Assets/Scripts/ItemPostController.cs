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
            return;
        else if(GameManager.Instance.CurrentMode == GameManager.PlayMode.Write)
        {
            if (!canvasPost.gameObject.activeSelf)
                GameManager.Instance.CurrentMode = GameManager.PlayMode.Play;
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out var hit, float.MaxValue, ItemLogic.itemLayerMask))
            {
                canvasPost.gameObject.SetActive(true);
                canvasPost.transform.Find("ButtonSpace").gameObject.SetActive(true);
                if (canvasPost.GetComponent<SaveAndGetData>().PostSaved)
                    canvasPost.transform.Find("TextSpace").gameObject.SetActive(true);
                else
                    canvasPost.transform.Find("InputField").gameObject.SetActive(true);
                GameManager.Instance.CurrentMode = GameManager.PlayMode.Write;
            }
        }

        if (canvasPost.transform.Find("TextSpace").gameObject.activeSelf)
            canvasPost.transform.Find("ButtonSpace").Find("show").GetComponent<Button>().interactable = false;
        else
            canvasPost.transform.Find("ButtonSpace").Find("show").GetComponent<Button>().interactable = true;


        if (canvasPost.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            canvasPost.gameObject.SetActive(false);
        }
    }
}
