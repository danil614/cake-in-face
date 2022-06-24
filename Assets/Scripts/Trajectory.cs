using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private int dotsNumber; // Количество точек в траектории
    [SerializeField] private GameObject dotPrefab; // Префаб точки
    [SerializeField] private float dotSpacing; // Расстояние между точками
    [SerializeField] [Range(0.01f, 0.3f)] private float dotMinScale; // Минимальный размер точек
    [SerializeField] [Range(0.1f, 1f)] private float dotMaxScale; // Максимальный размер точек

    private Transform[] _dotsList; // Массив точек для траектории

    private void Start()
    {
        Hide(); // Прячем траекторию
        PrepareDots(); // Подготавливаем точки для траектории
    }

    /// <summary>
    ///     Создает точки для траектории и рассчитывает их размер.
    /// </summary>
    private void PrepareDots()
    {
        _dotsList = new Transform[dotsNumber]; // Создаем массив точек траектории
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale; // Устанавливаем максимальный размер точек

        var scale = dotMaxScale;
        var scaleFactor = scale / dotsNumber;

        for (var i = 0; i < dotsNumber; i++)
        {
            _dotsList[i] = Instantiate(dotPrefab, transform).transform; // Создаем точку и группируем точку 
            _dotsList[i].localScale = Vector3.one * scale; // Устанавливаем новый размер точки

            if (scale > dotMinScale) scale -= scaleFactor; // Уменьшаем размер
        }
    }

    /// <summary>
    ///     Устанавливает позиции всех точек траектории по формуле.
    /// </summary>
    /// <param name="startPosition">Начальная позиция</param>
    /// <param name="forceApplied">Приложенная сила</param>
    public void UpdateDots(Vector3 startPosition, Vector2 forceApplied)
    {
        var timeStamp = dotSpacing;

        for (var i = 0; i < dotsNumber; i++)
        {
            // x = x0 + V0 * t - уравнение равномерного движения
            Vector2 position;
            position.x = startPosition.x + forceApplied.x * timeStamp;

            // y = y0 + V0 * t - g * t^2 / 2 - уравнение равноускоренного движения
            position.y = startPosition.y + forceApplied.y * timeStamp -
                         Physics2D.gravity.magnitude * timeStamp * timeStamp / 2f;

            _dotsList[i].position = position; // Применяем положение точки
            timeStamp += dotSpacing; // Увеличиваем время
        }
    }

    /// <summary>
    ///     Показывает траекторию.
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    ///     Прячет траекторию.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}