using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class ImageUpload : MonoBehaviour
    {
    public string url = "https://i.pinimg.com/originals/9e/1d/d6/9e1dd6458c89b03c506b384f537423d9.jpg";
    public RawImage rawImage;
    public TMP_InputField inputField;

    void OnEnable()
    {
        if (inputField.text.Contains("http"))
            url = inputField.text;
        StartCoroutine(DownloadImage(url));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        Debug.Log("Loading......");
        WWW wwwLoader = new WWW(url);
        yield return wwwLoader;

        Debug.Log(wwwLoader.url);
        rawImage.GetComponent<RawImage>().texture = wwwLoader.texture;
        if (rawImage.GetComponent<RawImage>().texture == wwwLoader.texture)
            Debug.Log(rawImage.GetComponent<RawImage>().texture);
    }

}