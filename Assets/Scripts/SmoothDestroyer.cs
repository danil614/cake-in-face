using System.Collections;
using UnityEngine;

public class SmoothDestroyer : MonoBehaviour
{
    [SerializeField] [Header("Скорость угасания объекта")]
    private float stepColor = 1;

    [SerializeField] [Header("Задержка удаления объекта")]
    private float delayColor = 20;

    private float _currentAlpha; // Текущий альфа канал
    private Color _currentColor; // Текущий цвет
    private bool _isDisappearing; // Исчезание
    private ObjectPool _objectPool; // Пул объектов
    private Color _originalColor; // Начальный цвет объекта
    private SpriteRenderer _spriteRenderer; // Спрайт

    private void Awake()
    {
        _objectPool = GameObject.Find(ObjectPool.ObjectPoolName).GetComponent<ObjectPool>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _isDisappearing = false;
    }

    private void Update()
    {
        if (_isDisappearing)
        {
            _currentAlpha -= Time.deltaTime * stepColor; // Уменьшаем альфа канал с каждым Update
            _currentColor.a = _currentAlpha; // Присваиваем альфа каналу новое значение
            _spriteRenderer.color = _currentColor;

            if (_currentAlpha <= 0.0f) // Когда объект не виден
            {
                _isDisappearing = false;
                _objectPool.ReturnObject(gameObject); // Убираем в пул объектов
            }
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        _isDisappearing = false;
        _spriteRenderer.color = _originalColor; // Устанавливаем изначальный цвет объекта
        _currentColor = _originalColor; // Устанавливаем текущий цвет объекта
        _currentAlpha = _originalColor.a; // Устанавливаем текущий альфа канал
    }

    /// <summary>
    ///     Начать плавное удаление объекта.
    /// </summary>
    public void StartDestroy(bool withDelay)
    {
        if (withDelay)
            StartCoroutine(Delay()); // Удаление с задержкой
        else
            _isDisappearing = true; // Удаление сразу
    }

    /// <summary>
    ///     Задержка угасания объекта.
    /// </summary>
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayColor); // Ждем время указанное в инспекторе перед началом исчезновения
        _isDisappearing = true;
    }
}