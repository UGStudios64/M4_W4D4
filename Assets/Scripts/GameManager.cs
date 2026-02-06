using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("// VICTORY -----------------------------------------------------------------------------------------")]
    [SerializeField] private Image fadeVictory;
    [SerializeField] private float victoryFadeTime;

    [Header("// GAMEOVER -----------------------------------------------------------------------------------------")]
    [SerializeField] private Image fadeGameOver;
    [SerializeField] private float gameOverFadeTime;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (!fadeGameOver) Debug.LogWarning($"Missing GameOver fade");
        if (!fadeVictory) Debug.LogWarning($"Missing Victory fade");

        fadeGameOver.canvasRenderer.SetAlpha(0f);
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-

    // VICTORY --------------------
    public void OnVictory()
    {
        fadeVictory.CrossFadeAlpha(1, victoryFadeTime, true);
        Invoke("Victory", victoryFadeTime);
    }
    private void Victory()
    { SceneManager.LoadScene("Victory"); }


    // DEATH --------------------
    public void OnDeath()
    {
        fadeGameOver.CrossFadeAlpha(1, gameOverFadeTime, true);
        Invoke("GameOver", gameOverFadeTime);
    }
    private void GameOver()
    { SceneManager.LoadScene("GameOver"); }
}