using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StartSelected : MonoBehaviour
{
    public GameObject firstButton;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Update()
    {
        if (!EventSystem.current.currentSelectedGameObject)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (h != 0 || v != 0)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }  
    }
}
