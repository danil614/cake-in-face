using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform heroCenter;

    [SerializeField]
    private float speed;

    [SerializeField]
    private CannonArea cannonArea;

    private bool isDragging = false; // Перетаскивание Hero

    private Rigidbody2D heroCenterRigidbody;

    private Vector2 startClick;
    private Vector2 endClick;

    /// <summary>
    /// Начато ли перетаскивание?
    /// </summary>
    [HideInInspector]
    public bool IsDragging { get => isDragging; }

    private void Start()
    {
        heroCenterRigidbody = heroCenter.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDragging)
        {
            if (!cannonArea.IsPressing)
            {
                endClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
            }

            //heroCenterRigidbody.MovePosition(endClick); // Перемещаем hero

            //heroCenterRigidbody.AddForce((endClick - startClick) * speed);
            //heroCenter.transform.Translate(speed * Time.fixedDeltaTime * (endClick - startClick));
            //var targetPosition = endClick;
            //heroCenter.position = Vector3.Lerp(startClick, targetPosition, speed);

            //heroCenter.position = Vector3.Lerp(heroCenter.position, endClick, speed * Time.deltaTime);

            //endClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем координаты нажатия
            heroCenterRigidbody.MovePosition(Vector2.Lerp(heroCenter.position, endClick, speed * Time.deltaTime));
        }
    }

    //private void FixedUpdate()
    //{

    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Hero"))
    //    {

    //    }
    //}

    /// <summary>
    /// Выполняется при нажатии на объект.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true; // Перетаскивание начато

        transform.parent = null; // Для изменения позиции убираем группировку от спрайта

        startClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Позиция нажатия
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
