using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MovementCannon movementCannon;
    [SerializeField] private CakeShooting cakeShooting;
    [SerializeField] private Trajectory trajectory;
    [SerializeField] private DragHero dragHero;
    [SerializeField] private DragCook[] dragCook;
    [SerializeField] private Transform turningPoint; // Точка поворота
    [SerializeField] private float pushForce; // Сила нажатия
    [SerializeField] private float startPushForce; // Стартовая сила нажатия
    private Vector2 _force; // Сила метания торта

    private bool _isClicking; // Нажатие на экран

    private Camera _mainCamera;
    private float _time; // Подсчет времени для роста траектории
    
    /// <summary>
    /// Открыто ли меню выбора?
    /// </summary>
    public bool IsMenuOpen { get; set; }

    private void Start()
    {
        _mainCamera = Camera.main;
        IsMenuOpen = false;
    }

    private void Update()
    {
        if (!IsHeroDragging() && !IsMenuOpen) // Если не перетаскиваем персонажа
        {
            if (Input.GetMouseButtonDown(0)) // Если кнопка была нажата
            {
                _isClicking = true;
                SetStartPushForce(); // Обнуляем время
                trajectory.Show(); // Показываем траекторию
            }

            if (_isClicking && Input.GetMouseButtonUp(0)) // Если кнопка была отпущена, но перед этим была нажата
            {
                _isClicking = false;
                cakeShooting.Push(_force); // Метаем торт
                trajectory.Hide(); // Прячем траекторию
                StartCoroutine(movementCannon.DoCannonKickback()); // Делаем отдачу пушки при стрельбе
            }

            if (_isClicking) // Если держим нажатие кнопки
            {
                _time += Time.deltaTime; // Считаем время нажатия
                OnClick(); // Обновление траектории при нажатии
            }
        }
    }

    /// <summary>
    ///     Проверяект есть ли перемещение какого-либо героя.
    /// </summary>
    private bool IsHeroDragging()
    {
        var isDragging = dragHero.IsDragging;
        if (isDragging) return true;

        foreach (var bodyPart in dragCook)
        {
            isDragging |= bodyPart.IsDragging;
            if (isDragging) break;
        }

        return isDragging;
    }

    /// <summary>
    ///     Устанавливает стартовую силу полета торта в зависимости от расстояния до нажатия.
    /// </summary>
    private void SetStartPushForce()
    {
        Vector2 startPoint = turningPoint.position;
        Vector2 endPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _time = Vector2.Distance(endPoint, startPoint) * startPushForce; // Устанваливаем стартовое время
    }

    /// <summary>
    ///     Построение траектории при нажатии.
    /// </summary>
    private void OnClick()
    {
        // Точки начала и конца направляющей линии траектории
        Vector2 startPoint = turningPoint.position;
        Vector2 endPoint = cakeShooting.Position;

        // Направляющий вектор длиной 1
        var direction = (endPoint - startPoint).normalized;

        // Сила метания торта
        _force = _time * pushForce * direction;

        // Расставить все точки по траектории полета
        trajectory.UpdateDots(cakeShooting.Position, _force);
    }

    /// <summary>
    ///     Запускает плавное удаление объекта.
    /// </summary>
    public static void StartSmoothDestroyer(GameObject gameObject, bool withDelay)
    {
        var smoothDestroyer = gameObject.GetComponent<SmoothDestroyer>();
        smoothDestroyer.StartDestroy(withDelay);
    }
}