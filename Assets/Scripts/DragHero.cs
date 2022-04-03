using UnityEngine;
using UnityEngine.EventSystems;

public class DragHero : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform heroCenter;

    [SerializeField]
    private float speed;

    [SerializeField]
    private CannonArea cannonArea;

    private bool isDragging = false; // Перетаскивание Hero

    private Rigidbody2D heroCenterRigidbody;
    //private Rigidbody2D heroRigidbody;

    //private Vector2 startClick;
    //private Vector2 endClick;

    private Camera mainCamera;

    /// <summary>
    /// Начато ли перетаскивание?
    /// </summary>
    [HideInInspector]
    public bool IsDragging { get => isDragging; }

    private void Start()
    {
        heroCenterRigidbody = heroCenter.GetComponent<Rigidbody2D>();
        //heroRigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (isDragging && !cannonArea.IsPressing)
        {
            Vector2 endClick = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
                                                                                   //heroCenterRigidbody.velocity = endClick * speed;
                                                                                   //heroRigidbody.velocity = (endClick - startClick) * speed;
                                                                                   //startClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //heroRigidbody.MovePosition(Vector2.Lerp(heroRigidbody.position, endClick, speed * Time.deltaTime));

            //heroRigidbody.MovePosition(endClick); // Перемещаем hero
            //transform.position = Vector3.Lerp(transform.position, endClick, speed * Time.fixedDeltaTime);

            heroCenterRigidbody.MovePosition(Vector2.Lerp(heroCenter.position, endClick, speed * Time.fixedDeltaTime));
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
