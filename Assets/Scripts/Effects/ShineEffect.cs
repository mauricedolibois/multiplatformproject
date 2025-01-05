using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonShineEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image buttonImage; 
    public Color shineColor = new Color(0.94f, 0.9f, 0.55f, 1f); // Khaki (#F0E68C)
    private Color originalColor;

    void Start()
    {
        if (buttonImage == null)
        {
            buttonImage = GetComponent<Image>(); // Auto-assign 
        }

        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }
        else
        {
            Debug.LogError("ButtonShineEffect: No Image component found. Assign one in the Inspector.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = shineColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = originalColor;
        }
    }
}