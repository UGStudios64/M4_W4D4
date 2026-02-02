using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDoor : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] SpriteRenderer logo;
    [SerializeField] Material Glow;
    CoinsHandler coinsHandler;

    [SerializeField] private int needCoin;

    private bool IsGlowing;

    private void Awake()
    {
        if (!player) Debug.LogError($"Missing Door Target");
        if (!logo) Debug.LogError($"Missing Logo"); 
    }

    private void Start()
    {
        coinsHandler = player.GetComponent<CoinsHandler>();
    }

    void Update()
    {
        if (coinsHandler.coins >= needCoin && !IsGlowing)
        {
            Debug.Log($"Logo Glow");
            logo.material = Glow;
            IsGlowing = true;
            return;
        }
    }
}