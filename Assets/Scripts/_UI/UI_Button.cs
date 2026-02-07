using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    Vector3 originalScale;
    TMP_Text text;

    [Header("// CHANGE COLOR ------------------------------------------------------------------------------------------")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;

    [Header("// ANIMATION ------------------------------------------------------------------------------------------")]
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float transitionSpeed;

    private bool isHovered;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Awake()
    {
        if (!text) text = GetComponentInChildren<TMP_Text>();
        originalScale = transform.localScale;
        text.color = normalColor;
    }

    void Update()
    {
        Vector3 targetScale = isHovered ? originalScale * scaleMultiplier : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * transitionSpeed);
    }

    // Mouse Hoover
    public void OnPointerEnter(PointerEventData eventData) => SetHover(true);
    public void OnPointerExit(PointerEventData eventData) => SetHover(false);

    // Controller Selected
    public void OnSelect(BaseEventData eventData) => SetHover(true);
    public void OnDeselect(BaseEventData eventData) => SetHover(false);


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void SetHover(bool hover)
    {
        isHovered = hover;
        text.color = hover ? hoverColor : normalColor;
    }
}