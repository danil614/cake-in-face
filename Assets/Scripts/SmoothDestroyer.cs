using System.Collections;
using UnityEngine;

public class SmoothDestroyer : MonoBehaviour
{
    private Color currentColor;
    private float currentAlpha;
    private bool isDisappearing = false;
    private SpriteRenderer spriteRenderer;
    private ObjectPool pool;

    [SerializeField] private float stepColor;
    [SerializeField] private float delayColor; // Задержка удаления объекта
    [SerializeField][Header("Пул объектов")] private GameObject objectPool;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pool = objectPool.GetComponent<ObjectPool>();
    }

    private void Start()
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
                pool.ReturnObject(gameObject); // Убираем в пул объектов
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayColor);
        isDisappearing = true;
    }
}
