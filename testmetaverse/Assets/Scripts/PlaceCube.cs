using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCube : MonoBehaviour
{
    public Cube[] cubeList;
    public Material[] materialList;
    public Cube prefabCube;
    public Material transparentMaterial;
    public Material cubeMaterial;

    public Vector3 debug;

    protected TPSCharacterController controller;
    protected Cube currentCube;
    protected bool positionOK;

    Ray ray;

    private void Awake()
    {
        controller = GetComponent<TPSCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetNextCube();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCube != null) 
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, CubeLogic.cubeLayerMask))
            {
                Debug.Log(currentCube == null ? "null" : "ok");
                // snap to grid
                Vector3 position = CubeLogic.SnapToGrid(hitInfo.point);

                Vector3 placePosition = position;
                positionOK = false;
                for (int i = 0; i < 10; i++)
                {
                    Collider[] collider = Physics.OverlapBox(placePosition + currentCube.transform.rotation * currentCube.boxCollider.center, currentCube.boxCollider.size / 2, currentCube.transform.rotation, CubeLogic.cubeLayerMask);
                    positionOK = collider.Length == 0;
                    if (positionOK)
                        break;
                    else
                        placePosition.y += CubeLogic.grid.y;
                }

                if (positionOK)
                    currentCube.transform.position = placePosition;
                else
                    currentCube.transform.position = position;

                // place the cube
                if(Input.GetMouseButtonDown(0)&&currentCube!=null&&positionOK)
                {
                    currentCube.boxCollider.enabled = true;
                    currentCube.SetMaterial(cubeMaterial);
                    Quaternion cubeRotation = currentCube.transform.rotation;
                    currentCube = null;
                    SetNextCube();
                    currentCube.transform.rotation = cubeRotation; 
                }

                // rotate cube
                if (Input.GetKeyDown(KeyCode.E))
                    currentCube.transform.Rotate(Vector3.up, 90);

                // delete brick
                if(Input.GetMouseButtonDown(1))
                {
                    if(Physics.Raycast(Camera.main.transform.position+Vector3.up*0.1f*controller.CameraDistance,Camera.main.transform.forward, out var hit, float.MaxValue, CubeLogic.cubeLayerMask))
                    {
                        var cube = hit.collider.GetComponent<Cube>();
                        if(cube!=null)
                        {
                            GameObject.DestroyImmediate(cube.gameObject);
                        }
                    }
                }
            }
        }
    }

    public void SetNextCube()
    {
        currentCube = Instantiate(prefabCube);
        currentCube.boxCollider.enabled = false;
        currentCube.SetMaterial(transparentMaterial);
    }

    public void SetPrefab(int cube, int mat)
    {
        prefabCube = cubeList[cube];
        cubeMaterial = materialList[mat];
    }
}
