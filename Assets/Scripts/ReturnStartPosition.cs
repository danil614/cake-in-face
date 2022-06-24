using UnityEngine;

public class ReturnStartPosition : MonoBehaviour
{
    [SerializeField] private float speed; // Скорость перемещения объекта

    private Vector2 _startPosition; // Стартовая позиция

    private void Start()
    {
        _startPosition = transform.position; // Устанавливаем начальную позицию объекта
    }

    private void Update()
    {
        // Плавно сдвигаем объект из текущей позиции в стартовую
        transform.position = Vector2.Lerp(transform.position, 
            _startPosition, speed * Time.deltaTime);
    }
}