using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveDoor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer logo;
    [SerializeField] private Material glow;
    [SerializeField] private Image fade;
    CoinsHandler coinsHandler;

    [Header("// FOR ACTIVE -----------------------------------------------------------------------------------------")]
    public int needCoin;
    private bool IsGlowing;
    private bool IsOpening;

    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (!anim) anim = GetComponentInChildren<Animator>();

        if (!player) Debug.LogError($"Missing Door Target");
        if (!logo) Debug.LogError($"Missing Logo");

        fade.canvasRenderer.SetAlpha(0f);
    }

    private void Start()
    {
        coinsHandler = player.GetComponent<CoinsHandler>();
    }

    void Update()
    {
        Glowing();
    }

    private void OnTriggerEnter(Collider other)
    {
        Opening(other);
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Glowing()
    {
        if (coinsHandler.coins >= needCoin && !IsGlowing)
        {
            Debug.Log($"Door Logo is Glowing");
            logo.material = glow;
            IsGlowing = true;
        }
    }

    private void Opening(Collider other)
    {
        if (other.CompareTag("Player") && IsGlowing && !IsOpening)
        {
            Debug.Log($"The Door is Opening");
            IsOpening = true;
            anim.SetBool("Opening", IsOpening);

            fade.CrossFadeAlpha(1, 2, true);
            Invoke("VictoryScene", 2f);
        }
    }

    public void VictoryScene()
    {
        SceneManager.LoadScene("Victory");
    }
}