using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveDoor : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer logo;
    [SerializeField] private Material glow;
    [Space(5)]
    [SerializeField] private GameObject player;
    [SerializeField] private UnityEvent OnVictory;
    private CoinsHandler coinsHandler;

    [Header("// FOR ACTIVE -----------------------------------------------------------------------------------------")]
    public int needCoin;
    private bool IsGlowing;
    private bool IsOpening;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (!player) Debug.LogWarning($"Missing Door Target");
        if (!logo) Debug.LogWarning($"Missing Logo");
        if (!glow) Debug.LogWarning($"Missing Glow");

        if (!anim) anim = GetComponentInChildren<Animator>();
    }

    void Start()
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

            IsGlowing = true;
            logo.material = glow;
        }
    }

    private void Opening(Collider other)
    {
        if (other.CompareTag("Player") && IsGlowing && !IsOpening)
        {
            Debug.Log($"The Door is Opening");

            IsOpening = true;
            anim.SetBool("Opening", IsOpening);
            OnVictory.Invoke();
        }
    }
}