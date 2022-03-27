using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 shiftCenterHero; // Смещение центра Hero
    private bool isDragging = false; // Перетаскивание Hero

    [HideInInspector]
    public bool IsDragging { get => isDragging; }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 endClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
            Vector2 shiftVector = endClick - shiftCenterHero; // Смещаем нажатие для смещения центра Hero
            GetComponent<Rigidbody2D>().MovePosition(shiftVector); // Перемещаем Hero
        }
    }

    /// <summary>
    /// Выполняется при нажатии на объект.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        shiftCenterHero = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Получаем смещение Hero
    }

    /// <summary>
    /// Выполняется при отпускании нажатия на объект.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}
