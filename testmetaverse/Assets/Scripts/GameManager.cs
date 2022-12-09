using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager singletonInstance;

    [HideInInspector]
    public enum PlayMode
    {
        Play,
        Build,
        Write
    }

    PlayMode currentMode;

    public static GameManager Instance
    {
        get
        {
            if(!singletonInstance)
            {
                singletonInstance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (singletonInstance == null)
                    Debug.Log("no Singleton obj");
            }

            return singletonInstance;
        }
    }

    void Awake()
    {
        if (singletonInstance == null)
            singletonInstance = this;
        else if (singletonInstance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Locked;
        currentMode = PlayMode.Play;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            currentMode = (currentMode == PlayMode.Play) ? PlayMode.Build : PlayMode.Play;
        }
    }

    public PlayMode CurrentMode
    {
        get { return currentMode; }
        set { currentMode = value; }
    }
}
