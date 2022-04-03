using UnityEngine;
using UnityEngine.EventSystems;

public class CannonArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isPressing = false;

    /// <summary>
    /// Показывает, есть ли нажатие в области пушки.
    /// </summary>
    [HideInInspector]
    public bool IsPressing { get { return isPressing; } }

    /// <summary>
    /// Срабатывает, если курсор в области объекта.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPressing = true;
    }

    /// <summary>
    /// Срабатывает, если курсор выходит из области объекта.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        isPressing = false;
    }
}
