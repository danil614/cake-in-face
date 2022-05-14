using UnityEngine;
using UnityEngine.EventSystems;

public class DragCook: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float speed; // Скорость перетаскивания hero
    [SerializeField] private CannonArea cannonArea; // Область пушки
    [SerializeField] private float shiftCenterOfMassY; // Смещение центра масс по Y

    private bool isDragging = false; // Перетаскивание Hero
    private Rigidbody2D bodyPartRigidbody; // Rigidbody2D части тела
    private Camera mainCamera;

    /// <summary>
    /// Начато ли перетаскивание?
    /// </summary>
    [HideInInspector] public bool IsDragging { get => isDragging; }

    private void Awake()
    {
        mainCamera = Camera.main;
        bodyPartRigidbody = GetComponent<Rigidbody2D>();
        if (shiftCenterOfMassY != 0) bodyPartRigidbody.centerOfMass = new Vector2(0, shiftCenterOfMassY); // Сдвигаем центр масс по Y
    }

    private void FixedUpdate()
    {
        if (isDragging && !cannonArea.IsPressing) // Если есть нажатие на hero, и hero не в области пушки
        {
            Vector2 endClick = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
            bodyPartRigidbody.MovePosition(Vector2.Lerp(bodyPartRigidbody.position, endClick, speed * Time.fixedDeltaTime)); // Плавно передвигаем
        }
    }

    /// <summary>
    /// Выполняется при нажатии на объект.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true; // Перетаскивание начато
    }

    /// <summary>
    /// Выполняется при отпускании нажатия на объект.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false; // Перетаскивание закончено
    }
}
