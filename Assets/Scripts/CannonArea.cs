using UnityEngine;
using UnityEngine.EventSystems;

public class CannonArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler
{
    private bool isPressing = false;

    [HideInInspector]
    public bool IsPressing { get { return isPressing; } }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("CannonArea OnDrag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("CannonArea OnPointerDown");
        isPressing = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("CannonArea OnPointerEnter");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("CannonArea OnPointerUp");
        isPressing = false;
    }
}
