using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Image portrait;
    public TextMeshProUGUI text;
    void Start()
    {
        //disable on start, will be shown/hidden by the dialogue triggers.
        gameObject.SetActive(false);
    }


    public void DisplayText(string content)
    {
        text.text = content;
    }

    public void DisplayPortrait(Sprite spr)
    {
        portrait.sprite = spr;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
