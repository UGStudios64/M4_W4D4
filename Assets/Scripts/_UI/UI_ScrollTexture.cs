using UnityEngine;
using UnityEngine.UI;

public class UI_ScrollTexture : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    { if (!image) image = GetComponent<RawImage>(); }

    void Update()
    { image.uvRect = new Rect(image.uvRect.position + new Vector2(speedX, speedY) * Time.deltaTime, image.uvRect.size); }
}