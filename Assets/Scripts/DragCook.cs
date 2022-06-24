using UnityEngine;
using UnityEngine.EventSystems;

public class DragCook : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float speed; // Скорость перетаскивания hero
    [SerializeField] private CannonArea cannonArea; // Область пушки
    [SerializeField] private float shiftCenterOfMassY; // Смещение центра масс по Y
    private Rigidbody2D _bodyPartRigidbody; // Rigidbody2D части тела

    private Camera _mainCamera;

    /// <summary>
    ///     Начато ли перетаскивание?
    /// </summary>
    public bool IsDragging { get; private set; }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _bodyPartRigidbody = GetComponent<Rigidbody2D>();
        if (shiftCenterOfMassY != 0)
            _bodyPartRigidbody.centerOfMass = new Vector2(0, shiftCenterOfMassY); // Сдвигаем центр масс по Y
    }

    private void FixedUpdate()
    {
        if (IsDragging && !cannonArea.IsPressing) // Если есть нажатие на hero, и hero не в области пушки
        {
            // Получаем координаты нажатия
            Vector2 endClick = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _bodyPartRigidbody.MovePosition(Vector2.Lerp(_bodyPartRigidbody.position, endClick,
                speed * Time.fixedDeltaTime)); // Плавно передвигаем
        }
    }

    /// <summary>
    ///     Выполняется при нажатии на объект.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        IsDragging = true; // Перетаскивание начато
    }

    /// <summary>
    ///     Выполняется при отпускании нажатия на объект.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragging = false; // Перетаскивание закончено
    }
}