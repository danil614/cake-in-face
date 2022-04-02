using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDestroyer : MonoBehaviour
{
    private Color color;
    private float alpha = 1.0f;
    private bool isDisappearing = false;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float stepColor;

    [SerializeField]
    private float delayColor; // Время для исчезновение пятна

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color; // Считывание цвета с объекта
        StartCoroutine(SlowDelete()); // Вызов корутины
    }

    private void Update()
    {
        if (isDisappearing)
        {
            alpha -= Time.deltaTime * stepColor; // Уменьшаем альфа канал с каждым Update
            color.a = alpha; // Присваиваем альфа каналу новое значение
            spriteRenderer.color = color;

            if (alpha <= 0.0f) // Когда пятна не видно, уничтожаем объект
            {
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator SlowDelete()
    {
        yield return new WaitForSeconds(delayColor);
        isDisappearing = true;
    }
}
