using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler
{
    private Vector2 shiftCenterHero; // Смещение центра Hero
    private bool isDragging = false;

    private void Update()
    {
        if (isDragging)
        {
            Vector2 endClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
            Vector2 shiftVector = endClick - shiftCenterHero; // Смещаем нажатие для смещения центра Hero
            GetComponent<Rigidbody2D>().MovePosition(shiftVector); // Перемещаем Hero
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //shiftCenterHero = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Получаем смещение Hero
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Vector2 endClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
        //Vector2 shiftVector = endClick - shiftCenterHero; // Смещаем нажатие для смещения центра Hero
        //GetComponent<Rigidbody2D>().MovePosition(shiftVector); // Перемещаем Hero
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //shiftCenterHero = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Получаем смещение Hero
        Debug.Log("Down");
        isDragging = true;
        shiftCenterHero = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Получаем смещение Hero
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
        isDragging = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Select");

    }
}
