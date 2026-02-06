using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private float fadeTime;
    [SerializeField] private bool startWith0Alpha;
    [Space (5)]
    [SerializeField] private GameObject firstButton;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (startWith0Alpha) fade.canvasRenderer.SetAlpha(0f);
    }

    void Update()
    {
        if (!EventSystem.current.currentSelectedGameObject)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (h != 0 || v != 0) EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Fade()
    { fade.CrossFadeAlpha(1, fadeTime, true); }


    // TESTING --------------------
    public void LoadSceneTesting()
    {
        Fade();
        Invoke("Testing", fadeTime);
    }
    private void Testing()
    { SceneManager.LoadScene("Testing"); }


    // TITLE SCREEN --------------------
    public void LoadSceneTitleScreen()
    {
        Fade();
        Invoke("TitleScreen", fadeTime);
    }
    private void TitleScreen()
    { SceneManager.LoadScene("TitleScreen"); }
}