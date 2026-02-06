using UnityEngine;
using UnityEngine.UI;

public class UI_Lifebar : MonoBehaviour
{ 
    [SerializeField] private Image fillLife;

    [Header("// FILL RANGE -----------------------------------------------------------------------------------------")]
    [SerializeField] private float minFill;
    [SerializeField] private float maxFill;
    [HideInInspector] public bool InDanger; // No,I am the danger!

    [Header("// PULSE IN DANGER -----------------------------------------------------------------------------------------")]
    [SerializeField] [Range(0.1f, 1f)] private float lifePercent;
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float pulseAmount;
    Vector3 baseScale;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        baseScale = transform.localScale;
    }
    
    void Update()
    {
        if (InDanger) // Make the lifebar pulsing
        {
            float pulse = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = baseScale * pulse;
        }
        else transform.localScale = Vector3.Lerp(transform.localScale, baseScale, Time.deltaTime * 10f); 
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    public void UpdateLifeBar(int currentHP, int maxHP)
    {
        float percent = (float)currentHP / maxHP;
        fillLife.fillAmount = Mathf.Lerp(minFill, maxFill, percent);

        InDanger = percent <= lifePercent;
    }
}