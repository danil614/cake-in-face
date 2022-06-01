using UnityEngine;
using UnityEngine.EventSystems;

public class CannonArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    ///     Показывает, есть ли нажатие в области пушки.
    /// </summary>
    public bool IsPressing { get; private set; }

    /// <summary>
    ///     Срабатывает, если курсор в области объекта.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsPressing = true;
    }

    /// <summary>
    ///     Срабатывает, если курсор выходит из области объекта.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        IsPressing = false;
    }
}