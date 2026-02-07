using UnityEngine;
using UnityEngine.UI;

public class UI_Fade : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    private Image fade;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Awake()
    { fade = GetComponent<Image>(); }
    
    void Start()
    { fade.CrossFadeAlpha(0, fadeTime, true); }
}