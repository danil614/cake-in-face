using UnityEngine;

public class RotateCannon : MonoBehaviour
{
    [SerializeField] private Transform turningPoint; // Точка поворота пушки
    [SerializeField] private float shiftAngle; // Сдвиг угла поворота пушки
    [SerializeField] private float turningSpeed; // Скорость поворота пушки
    [SerializeField] private float minAngle; // Минимальный угол поворота
    [SerializeField] private float maxAngle; // Максимальный угол поворота

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        TurnCannon(); // Поворачиваем пушку
    }

    /// <summary>
    /// Получает параллельный вектор.
    /// </summary>
    private Vector2 GetParallelVector(Vector2 direction)
    {
        // Расстояние по координатам между точек
        float distanceX = turningPoint.position.x - transform.position.x;
        float distanceY = turningPoint.position.y - transform.position.y;

        // Параллельный перенос вектора
        Vector2 parallelVector = new Vector2(direction.x - distanceX, direction.y - distanceY);
        return parallelVector;
    }

    /// <summary>
    /// Поворачивает пушку в сторону нажатия.
    /// </summary>
    private void TurnCannon()
    {
        Vector3 clickedPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Получаем позицию нажатия
        Vector3 direction = clickedPosition - transform.position; // Получаем направляющий вектор
        
        Vector2 parallelVector = GetParallelVector(direction); // Получаем параллельный вектор
        
        float angle = Mathf.Atan2(parallelVector.y, parallelVector.x) * Mathf.Rad2Deg; // Получаем угол параллельной прямой
        float limitedAngle = Mathf.Clamp(angle + shiftAngle, minAngle, maxAngle); // Ограничиваем угол поворота
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, limitedAngle), Time.deltaTime * turningSpeed); // Плавно поворачиваем
    }
}
