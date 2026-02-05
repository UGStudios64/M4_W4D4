using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UI_Lifebar : MonoBehaviour
{ 
    [SerializeField] private Image fillLife;
    [SerializeField] private Image glow;

    [Header("// FILL RANGE -----------------------------------------------------------------------------------------")]
    [SerializeField] private float minFill;
    [SerializeField] private float maxFill;
    [HideInInspector] public bool InDanger; // No,I am the danger!

    [Header("// PULSE IN DANGER -----------------------------------------------------------------------------------------")]
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float pulseAmount;
    Vector3 baseScale;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        baseScale = transform.localScale;
        glow.canvasRenderer.SetAlpha(0f);
    }
    
    void Update()
    {
        if (InDanger)
        {
            float pulse = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = baseScale * pulse;
            glow.CrossFadeAlpha(1, 5, true);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, baseScale, Time.deltaTime * 10f);
            glow.CrossFadeAlpha(0, 2, true);
        }
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    public void UpdateLifeBar(int currentHP, int maxHP)
    {
        float percent = (float)currentHP / maxHP;
        fillLife.fillAmount = Mathf.Lerp(minFill, maxFill, percent);

        InDanger = percent <= 0.3f;

        if (InDanger) glow.CrossFadeAlpha(1, 5, true);
        else glow.CrossFadeAlpha(0, 3, true);
    }
}