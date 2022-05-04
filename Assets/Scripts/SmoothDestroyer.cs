using System.Collections;
using UnityEngine;

public class SmoothDestroyer : MonoBehaviour
{
    private Color originalColor; // Изначальный цвет объекта
    private float currentAlpha; // Текущий альфа канал
    private Color currentColor; // Текущий цвет
    private bool isDisappearing; // Исчезание
    private SpriteRenderer spriteRenderer; // Спрайт
    private ObjectPool objectPool; // Пул объектов

    [SerializeField][Header("Скорость угасания объекта")] private float stepColor = 1;
    [SerializeField][Header("Задержка удаления объекта")] private float delayColor = 20;

    private void Awake()
    {
        objectPool = GameObject.Find(ObjectPool.ObjectPoolName).GetComponent<ObjectPool>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        isDisappearing = false;
    }

    /// <summary>
    /// Начать плавное удаление объекта.
    /// </summary>
    public void StartDestroy(bool withDelay)
    {
        if (withDelay)
        {
            StartCoroutine(Delay()); // Удаление с задержкой
        }
        else
        {
            isDisappearing = true; // Удаление сразу
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        isDisappearing = false;
        spriteRenderer.color = originalColor; // Устанавливаем изначальный цвет объекта
        currentColor = originalColor; // Устанавливаем текущий цвет объекта
        currentAlpha = originalColor.a; // Устанавливаем текущий альфа канал
    }

    /// <summary>
    /// Задержка угасания объекта.
    /// </summary>
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayColor);
        isDisappearing = true;
    }

    private void Update()
    {
        if (isDisappearing)
        {
            currentAlpha -= Time.deltaTime * stepColor; // Уменьшаем альфа канал с каждым Update
            currentColor.a = currentAlpha; // Присваиваем альфа каналу новое значение
            spriteRenderer.color = currentColor;

            if (currentAlpha <= 0.0f) // Когда объект не виден
            {
                isDisappearing = false;
                objectPool.ReturnObject(gameObject); // Убираем в пул объектов
            }
        }
    }
}
