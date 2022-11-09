using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public int SNum;

    public void OnClick()
    {
        SceneManager.LoadSceneAsync(SNum);
    }
}
