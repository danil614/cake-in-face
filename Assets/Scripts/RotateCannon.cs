using UnityEngine;

public class RotateCannon : MonoBehaviour
{
    [SerializeField] private Transform turningPoint; // Точка поворота пушки
    [SerializeField] private float shiftAngle; // Сдвиг угла поворота пушки
    [SerializeField] private float turningSpeed; // Скорость поворота пушки
    [SerializeField] private float minAngle; // Минимальный угол поворота
    [SerializeField] private float maxAngle; // Максимальный угол поворота

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        TurnCannon(); // Поворачиваем пушку
    }

    /// <summary>
    ///     Получает параллельный вектор.
    /// </summary>
    private Vector2 GetParallelVector(Vector2 direction)
    {
        // Расстояние по координатам между точек
        var positionTurningPoint = turningPoint.position;
        var positionTransform = transform.position;
        var distanceX = positionTurningPoint.x - positionTransform.x;
        var distanceY = positionTurningPoint.y - positionTransform.y;

        // Параллельный перенос вектора
        var parallelVector = new Vector2(direction.x - distanceX, direction.y - distanceY);
        return parallelVector;
    }

    /// <summary>
    ///     Поворачивает пушку в сторону нажатия.
    /// </summary>
    private void TurnCannon()
    {
        var clickedPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition); // Получаем позицию нажатия
        var direction = clickedPosition - transform.position; // Получаем направляющий вектор

        var parallelVector = GetParallelVector(direction); // Получаем параллельный вектор

        var angle = Mathf.Atan2(parallelVector.y, parallelVector.x) *
                    Mathf.Rad2Deg; // Получаем угол параллельной прямой
        var limitedAngle = Mathf.Clamp(angle + shiftAngle, minAngle, maxAngle); // Ограничиваем угол поворота

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, limitedAngle),
            Time.deltaTime * turningSpeed); // Плавно поворачиваем
    }
}