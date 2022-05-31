using UnityEngine;

public class CakeShooting : MonoBehaviour
{
    [SerializeField] [Header("Префабы торта")]
    private GameObject[] cakePrefabs;

    [SerializeField] [Header("Удаление торта по количеству")]
    private DestroyerByNumber destroyerByNumber;

    [SerializeField] [Header("Пул объектов")]
    private ObjectPool objectPool;

    [SerializeField] [Header("Точка старта перезарядки")]
    private Transform reloadPoint;

    [SerializeField] [Header("Скорость перезарядки")]
    private float speed;

    private GameObject _currentCake;
    private CapsuleCollider2D _currentCakeCollider;
    private Rigidbody2D _currentCakeRigidbody;

    private bool _isReloading; // Перезарядка

    public Vector3 Position => transform.position;

    private void Start()
    {
        _isReloading = false;
        ReloadCake(); // Перезаряжаем пушку
    }

    private void Update()
    {
        if (_isReloading) // Если перезарядка
        {
            // Плавно сдвигаем объект из текущей позиции в стартовую
            _currentCake.transform.position = Vector2.Lerp(_currentCake.transform.position, transform.position,
                speed * Time.deltaTime);

            if (Vector3.Distance(_currentCake.transform.position, transform.position) <= 0.05f) // Если торт достиг точки
                _isReloading = false; // Выключаем перезарядку
        }
    }

    /// <summary>
    ///     Задает ему силу метания торта.
    /// </summary>
    /// <param name="force">Сила метания</param>
    public void Push(Vector2 force)
    {
        _isReloading = false; // Останавливаем перезарядку
        _currentCake.transform.parent = null; // Убираем группировку
        _currentCakeRigidbody.isKinematic = false; // Включаем Rigidbody
        
        // Устанавливаем скорость полета торта
        _currentCakeRigidbody.AddForce(force, ForceMode2D.Impulse);
        _currentCakeCollider.enabled = true; // Включаем коллайдер

        // Добавляем торт в коллекцию
        destroyerByNumber.AddToCollection(_currentCake);
        // Запускаем плавное удаление торта по времени
        GameManager.StartSmoothDestroyer(_currentCake, true);
        ReloadCake(); // Перезаряжаем пушку
    }

    /// <summary>
    ///     Перезаряжает пушку тортом.
    /// </summary>
    private void ReloadCake()
    {
        var cakePrefab = cakePrefabs[Random.Range(0, cakePrefabs.Length)];
        _currentCake =
            objectPool.GetObject(cakePrefab, reloadPoint.position, transform.rotation, transform); // Создаем новый торт
        _currentCakeRigidbody = _currentCake.GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
        _currentCakeRigidbody.isKinematic = true;

        _currentCakeCollider = _currentCake.GetComponent<CapsuleCollider2D>(); // Получаем Collider2D
        _currentCakeCollider.enabled = false; // Выключаем коллайдер

        _isReloading = true;
    }
}