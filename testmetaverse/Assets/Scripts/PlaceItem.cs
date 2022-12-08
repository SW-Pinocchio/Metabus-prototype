using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class PlaceItem : MonoBehaviour
{
    [Tooltip("prefab item 리스트를 갖고 있는 상위 prefab")]
    public Item placeItem;
    public Material[] materialList;
    public Material transparentMaterial;
    int itemIndex = 0;

    public Vector3 debug;

    protected TPSCharacterController controller;
    protected Item currentItemPlace;
    protected bool positionOK;

    Ray ray;

    private void Awake()
    {
        controller = GetComponent<TPSCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetNextItem();
    }

    // Update is called once per frame
    void Update()
    {
        // scroll to activate item
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            itemIndex++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            itemIndex--;
        }
        itemIndex = Mathf.Clamp(itemIndex, 0, 102);
        ActiveItem();

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (currentItemPlace != null)
        {
            if (Physics.Raycast(ray, out var hit, float.MaxValue, ItemLogic.itemLayerMask))
            {
                // snap to grid
                var position = ItemLogic.SnapToGrid(hit.point);

                // try to find a collision free position
                Vector3 placePosition = position;
                positionOK = false;
                for (int i = 0; i < 10; i++)
                {
                    Collider[] collider = Physics.OverlapBox(placePosition + currentItemPlace.transform.rotation * currentItemPlace.BoxCollider.center,
                        currentItemPlace.BoxCollider.size / 2, currentItemPlace.transform.rotation, ItemLogic.itemLayerMask);
                    positionOK = collider.Length == 0;
                    if (positionOK)
                        break;
                    else
                        placePosition.y += ItemLogic.grid.y;
                }

                if (positionOK)
                    currentItemPlace.transform.position = placePosition;
                else
                    currentItemPlace.transform.position = position;
            }
        }

        // place the brick
        if (Input.GetMouseButtonDown(0) && currentItemPlace != null && positionOK)
        {
            currentItemPlace.BoxCollider.enabled = true;

            Quaternion itemRotation = currentItemPlace.transform.rotation;
            currentItemPlace = null;
            SetNextItem();
            currentItemPlace.transform.rotation = itemRotation;
        }

        // rotate item
        if (Input.GetKeyDown(KeyCode.E))
            currentItemPlace.transform.Rotate(Vector3.up, 90);

        // delete brick
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out var hit, float.MaxValue, ItemLogic.itemLayerMask))
            {
                var item = hit.collider.GetComponent<Item>();
                if (item != null)
                {
                    GameObject.DestroyImmediate(item.gameObject);
                }
            }
        }
    }

    public void SetNextItem()
    {
        currentItemPlace = Instantiate(placeItem);
        currentItemPlace.transform.GetChild(itemIndex).gameObject.SetActive(true);
        ActiveItem();

        Debug.Log(currentItemPlace.name);
        currentItemPlace.BoxCollider.enabled = false;
        currentItemPlace.SetMaterial(transparentMaterial);
    }

    void ActiveItem()
    {
        Transform[] itemList = currentItemPlace.GetComponentsInChildren<Transform>(true);
        Transform[] currentItemChild = currentItemPlace.transform.GetChild(itemIndex).GetComponentsInChildren<Transform>(true);

        foreach (Transform item in itemList)
        {
            if (item.name == currentItemPlace.name)
                continue;
            item.gameObject.SetActive(false);
        }
        foreach (Transform child in currentItemChild)
        {
            child.gameObject.SetActive(true);
        }
        Debug.Log(currentItemPlace.transform.GetChild(itemIndex).gameObject.name);
    }
}
