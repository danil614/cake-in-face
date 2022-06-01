using System.Collections;
using UnityEngine;

public class SmoothDestroyer : MonoBehaviour
{
    private Color currentColor; //RGBA объекта
    private float currentAlpha; //Альфа канал
    private bool isDisappearing; //Переменная для начала исчезновения
    private SpriteRenderer spriteRenderer; //Рендер спрайта

    private float stepColor;  // Скорость угасания объекта
    private float delayColor; // Задержка удаления объекта
    private ObjectPool objectPool; // Пул объектов

    [HideInInspector] public float StepColor { set { stepColor = value; } }
    [HideInInspector] public float DelayColor { set { delayColor = value; } }
    [HideInInspector] public ObjectPool ObjectPool { set { objectPool = value; } }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isDisappearing = false;
    }

    public void StartSmoothDestroyer()
    {
        currentColor = spriteRenderer.color; // Считывание цвета с объекта
        currentAlpha = currentColor.a; // Получаем альфа канал
        StartCoroutine(Delay()); // Задержка удаления
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
                ResetSettings();
                objectPool.ReturnObject(gameObject); // Убираем в пул объектов
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayColor);  //Ждем время указанное в инспекторе перед началом исчезновения 
        isDisappearing = true;
    }

    public void ResetSettings()
    {
        StopCoroutine(Delay());
        isDisappearing = false;
        currentColor.a = 1.0f; // Присваиваем альфа каналу стандартное значение
        spriteRenderer.color = currentColor; //Восстанавливаем RGBA
    }
}
