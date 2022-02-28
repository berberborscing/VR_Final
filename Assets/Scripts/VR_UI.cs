using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class VR_UI : MonoBehaviour
{
    public GameObject textDisplay;
    public bool show = true;

    public void OnButtonPress()
    {
        textDisplay.SetActive(!show);
        show = !show;
    }
}
