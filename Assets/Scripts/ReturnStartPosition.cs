using UnityEngine;

public class ReturnStartPosition : MonoBehaviour
{
    [SerializeField]
    private float speed; // Скорость перемещения объекта

    private Vector2 startPosition; // Стартовая позиция

    private void Start()
    {
        startPosition = transform.position; // Устанавливаем начальную позицию объекта
    }

    private void Update()
    {
        // Плавно сдвигаем объект из текущей позиции в стартовую
        transform.position = Vector2.Lerp(transform.position, startPosition, speed * Time.deltaTime);
    }
}
