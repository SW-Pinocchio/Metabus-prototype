using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObjectInteractionControler : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    Animator animator;

    [SerializeField]
    Camera mainCamera;
    GameObject raycast;
    Vector3 mousePosition; // 클릭 시 마우스 위치
    Vector3 objectPosition; // 오브젝트를 둘 위치

    // Start is called before the first frame update
    void Awake()
    {
        animator = characterBody.GetComponent<Animator>();
        raycast = transform.Find("RayBox").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        placeCursorBlocks();
    }

    private void placeCursorBlocks()
    {

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isInteracting", true);

            mousePosition = Input.mousePosition;
            //mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            //objectPosition = Camera.main.ViewportToWorldPoint(
            //    new Vector3(0.5f - mousePosition.x, 0.5f - mousePosition.y, -mousePosition.z));
            //objectPosition = new Vector3(mousePosition.x, mousePosition.y, -mousePosition.z);
            //Instantiate(prefab, objectPosition, Quaternion.identity); 
        }
        else if (Input.GetMouseButtonUp(0)) 
            animator.SetBool("isInteracting", false);
    }
}
