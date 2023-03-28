using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextOverlayManager : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator temporaryText(string text, float duration)
    {
        Debug.Log("started coroutine");
        //set text
        textMeshProUGUI.text = text;

        Debug.Log("puasing for" + duration + " seconds");
        //start countdown
        yield return new WaitForSeconds(duration);

        Debug.Log("turning invisible");
        //return back to invisible
        textMeshProUGUI.text = "";
    }

    public void startTextOverlay(int number)
    {
        StartCoroutine(temporaryText(number.ToString(), 2f));
    }

}
