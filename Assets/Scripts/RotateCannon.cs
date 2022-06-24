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
    ///     Поворачивает пушку в сторону нажатия.
    /// </summary>
    private void TurnCannon()
    {
        // Получаем позицию нажатия
        var clickedPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // Получаем вектор для вычисления угла
        var direction = clickedPosition - turningPoint.position;
        // Получаем угол между вектором и осью Х
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Ограничиваем угол поворота
        var limitedAngle = Mathf.Clamp(angle + shiftAngle, minAngle, maxAngle);

        transform.rotation = Quaternion.Lerp(transform.rotation, 
            Quaternion.Euler(0, 0, limitedAngle),
            Time.deltaTime * turningSpeed); // Плавно поворачиваем
    }
}