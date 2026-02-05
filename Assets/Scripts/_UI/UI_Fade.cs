using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Fade : MonoBehaviour
{
    private Image fade;

    private void Awake()
    {
        fade = GetComponent<Image>();
    }
    
    private void Start()
    {
        fade.CrossFadeAlpha(0, 2, true);
    }
}