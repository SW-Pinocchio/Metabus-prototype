using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class SaveAndGetData : MonoBehaviour
    {
    [SerializeField] TMP_InputField titleInput;
    [SerializeField] TMP_InputField contentInput;
    [SerializeField] TMP_Text titleResultText;
    [SerializeField] TMP_Text contentResultText;
    public string postListURL = "http://localhost/HighScoreGame/display.php";
    public string addPostURL = "http://localhost/HighScoreGame/addpost.php?";
    public string editPostURL = "http://localhost/HighScoreGame/editpost.php?";


    public void SubmitData()
    {
        StartCoroutine(SetPosts(titleInput.text, contentInput.text));
    }
    public void ShowData()
    {
        titleResultText.text = "";
        contentResultText.text = "";
        StartCoroutine(GetPosts());
    }

    public void EditData()
    {
        titleResultText.text = "";
        contentResultText.text = "";
        StartCoroutine(GetPosts());
        titleInput.text = titleResultText.text;
        contentInput.text = contentResultText.text;
    }

    IEnumerator GetPosts()
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(postListURL);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null)
            Debug.Log(hs_get.error);
        else
        {
            string dataText = hs_get.downloadHandler.text;
            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                for (int i = 0; i < mc.Count; i++)
                {
                    if (i % 2 == 0)
                        titleResultText.text += splitData[i];
                    else
                        contentResultText.text += splitData[i];
                }
            }
        }
    }

    IEnumerator SetPosts(string name, string content)
    {
        string hash = HashInput(name + content);
        string post_url = addPostURL + "title=" + UnityWebRequest.EscapeURL(name) + "&content=" + content;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log(hs_post.error);
    }

    IEnumerator EditPosts(string name, string content)
    {
        string hash = HashInput(name + content);
        string post_url = editPostURL + "title=" + UnityWebRequest.EscapeURL(name) + "&content=" + content;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log(hs_post.error);
    }

    public string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue =
            hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert =
            BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        return hash_convert;
    }
}