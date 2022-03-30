using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform heroCenter;

    //private Vector2 shiftCenterHero; // Смещение центра Hero
    private bool isDragging = false; // Перетаскивание Hero

    [HideInInspector]
    public bool IsDragging { get => isDragging; }

    private void Start()
    {
        //transform.parent = null;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 endClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
            //Vector2 shiftVector = endClick - shiftCenterHero; // Смещаем нажатие для смещения центра Hero
            //GetComponent<Rigidbody2D>().MovePosition(shiftVector); // Перемещаем Hero

            //heroCenter.GetComponent<Rigidbody2D>().MovePosition(endClick);
            heroCenter.GetComponent<Rigidbody2D>().MovePosition(endClick);
        }
    }

    /// <summary>
    /// Выполняется при нажатии на объект.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Start OnPointerDown");
        isDragging = true;
        //shiftCenterHero = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Получаем смещение Hero

        //spriteHero.parent = null;
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //spriteHero.parent = transform;

        transform.parent = null;

        Vector2 startClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        heroCenter.transform.position = startClick;
        heroCenter.transform.rotation = transform.rotation; //Quaternion.Euler(Vector3.zero);

        transform.parent = heroCenter;

        Debug.Log("Stop OnPointerDown");
    }

    /// <summary>
    /// Выполняется при отпускании нажатия на объект.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Start OnPointerUp");
        isDragging = false;
        Debug.Log("Stop OnPointerUp");
    }
}
