using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI printingText;
    string text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>().text;
        Debug.Log("Title: " + text);
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        //yield return new WaitForSeconds(0.15f);
        for (int i = 0; i <= text.Length; i++)
        {
            printingText.text = text.Substring(0, i);
            yield return new WaitForSeconds(0.15f); 
        }
    }
}
