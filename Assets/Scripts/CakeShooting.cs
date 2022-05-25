using UnityEngine;

public class CakeShooting : MonoBehaviour
{
	[SerializeField][Header("Префабы торта")] private GameObject[] cakePrefabs;
	[SerializeField][Header("Удаление торта по количеству")] private DestroyerByNumber destroyerByNumber;
	[SerializeField][Header("Пул объектов")] private ObjectPool objectPool;
	[SerializeField][Header("Точка старта перезарядки")] private Transform reloadPoint;
	[SerializeField][Header("Скорость перезарядки")] private float speed;

	[HideInInspector] public Vector3 Position { get => transform.position; }

	private GameObject currentCake;
	private Rigidbody2D currentCakeRigidbody;
	private CapsuleCollider2D currentCakeCollider;

	private bool isReloading; // Перезарядка

    private void Start()
    {
		isReloading = false;
		ReloadCake(); // Перезаряжаем пушку
    }

    private void Update()
    {
        if (isReloading) // Если перезарядка
        {
			// Плавно сдвигаем объект из текущей позиции в стартовую
			currentCake.transform.position = Vector2.Lerp(currentCake.transform.position, transform.position, speed * Time.deltaTime);

			if (Vector3.Distance(currentCake.transform.position, transform.position) <= 0.05f) // Если торт достиг точки
			{
				isReloading = false; // Выключаем перезарядку
            }
		}
	}

	/// <summary>
	/// Создает торт и задает ему силу метания.
	/// </summary>
	/// <param name="force">Сила метания</param>
	public void Push(Vector2 force)
	{
		isReloading = false; // Останавливаем перезарядку

		currentCake.transform.parent = null; // Убираем группировку

		currentCakeRigidbody.isKinematic = false; // Включаем Rigidbody
		// Устанавливаем скорость полета торта
		currentCakeRigidbody.AddForce(force, ForceMode2D.Impulse);
		currentCakeCollider.enabled = true; // Включаем коллайдер

		// Добавляем торт в коллекцию
		destroyerByNumber.AddToCollection(currentCake);
		// Запускаем плавное удаление торта по времени
		GameManager.StartSmoothDestroyer(currentCake, true);
		ReloadCake(); // Перезаряжаем пушку
	}

	/// <summary>
	/// Перезаряжает пушку тортом.
	/// </summary>
	private void ReloadCake()
    {
		GameObject cakePrefab = cakePrefabs[Random.Range(0, cakePrefabs.Length)];
		currentCake = objectPool.GetObject(cakePrefab, reloadPoint.position, transform.rotation, transform); // Создаем новый торт
		currentCakeRigidbody = currentCake.GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
		currentCakeRigidbody.isKinematic = true;

		currentCakeCollider = currentCake.GetComponent<CapsuleCollider2D>(); // Получаем Collider2D
		currentCakeCollider.enabled = false; // Выключаем коллайдер

		isReloading = true;
	}
}
