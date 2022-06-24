using UnityEngine;
using UnityEngine.EventSystems;

public class DragHero : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform heroCenter; // Точка поворота hero
    [SerializeField] private float speed; // Скорость перетаскивания hero
    [SerializeField] private CannonArea cannonArea; // Область пушки
    [SerializeField] private float shiftCenterOfMassY; // Смещение центра масс по Y
    private Rigidbody2D _heroCenterRigidbody; // Rigidbody2D точки поворота hero

    private Camera _mainCamera;

    /// <summary>
    ///     Начато ли перетаскивание?
    /// </summary>
    public bool IsDragging { get; private set; }

    private void Start()
    {
        _mainCamera = Camera.main;

        _heroCenterRigidbody = heroCenter.GetComponent<Rigidbody2D>();

        var heroRigidbody = GetComponent<Rigidbody2D>(); // Получаем компонент
        heroRigidbody.centerOfMass = new Vector2(0, shiftCenterOfMassY); // Сдвигаем центр масс по Y
    }

    private void FixedUpdate()
    {
        if (IsDragging && !cannonArea.IsPressing) // Если есть нажатие на hero, и hero не в области пушки
        {
            Vector2 endClick = _mainCamera.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
            _heroCenterRigidbody.MovePosition(Vector2.Lerp(heroCenter.position, endClick,
                speed * Time.fixedDeltaTime)); // Плавно передвигаем
        }
    }

    /// <summary>
    ///     Выполняется при нажатии на объект.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        IsDragging = true; // Перетаскивание начато

        transform.parent = null; // Для изменения позиции убираем группировку от спрайта

        Vector2 startClick = _mainCamera.ScreenToWorldPoint(Input.mousePosition); // Позиция нажатия
        
        heroCenter.SetPositionAndRotation(new Vector3(startClick.x, startClick.y, 0),
            transform.rotation); // Устанавливаем позицию и угол поворота
        
        // Обнуляем координату Z для Rect Transform
        var localPosition = heroCenter.localPosition;
        heroCenter.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
        
        transform.parent = heroCenter; // Ставим группировку для спрайта
    }

    /// <summary>
    ///     Выполняется при отпускании нажатия на объект.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragging = false; // Перетаскивание закончено
    }
}