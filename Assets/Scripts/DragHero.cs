using UnityEngine;
using UnityEngine.EventSystems;

public class DragHero : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform heroCenter; // Точка поворота hero

    [SerializeField]
    private float speed; // Скорость перетаскивания hero

    [SerializeField]
    private CannonArea cannonArea; // Область пушки

    [SerializeField]
    private float shiftCenterOfMassY; // Смещение центра масс по Y

    private bool isDragging = false; // Перетаскивание Hero
    private Rigidbody2D heroCenterRigidbody; // Rigidbody2D точки поворота hero
    private Camera mainCamera;

    /// <summary>
    /// Начато ли перетаскивание?
    /// </summary>
    [HideInInspector]
    public bool IsDragging { get => isDragging; }

    private void Start()
    {
        mainCamera = Camera.main;

        heroCenterRigidbody = heroCenter.GetComponent<Rigidbody2D>();
        
        Rigidbody2D heroRigidbody = GetComponent<Rigidbody2D>(); // Получаем компонент
        heroRigidbody.centerOfMass = new Vector2(0, shiftCenterOfMassY); // Сдвигаем центр масс по Y
    }

    private void FixedUpdate()
    {
        if (isDragging && !cannonArea.IsPressing) // Если есть нажатие на hero, и hero не в области пушки
        {
            Vector2 endClick = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
            heroCenterRigidbody.MovePosition(Vector2.Lerp(heroCenter.position, endClick, speed * Time.fixedDeltaTime)); // Плавно передвигаем
        }
    }

    /// <summary>
    /// Выполняется при нажатии на объект.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true; // Перетаскивание начато

        transform.parent = null; // Для изменения позиции убираем группировку от спрайта

        Vector2 startClick = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Позиция нажатия
        heroCenter.SetPositionAndRotation(startClick, transform.rotation); // Устанавливаем позицию и угол поворота

        transform.parent = heroCenter; // Ставим группировку для спрайта
    }

    /// <summary>
    /// Выполняется при отпускании нажатия на объект.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false; // Перетаскивание закончено
    }
}
