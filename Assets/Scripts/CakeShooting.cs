using UnityEngine;

public class CakeShooting : MonoBehaviour
{
	[SerializeField] private GameObject cakePrefab; // Префаб торта
	[SerializeField][Header("Удаление торта по количеству")] private Destroyer destroyer;
	[SerializeField][Header("Пул объектов")] private ObjectPool objectPool;
	[SerializeField][Header("Скорость угасания торта")] private float stepColor;
	[SerializeField][Header("Задержка удаления торта")] private float delayColor;

	[SerializeField][Header("Точка старта перезарядки")] private Transform reloadPoint;
	[SerializeField][Header("Скорость перезарядки")] private float speed;

	[HideInInspector] public Vector3 Position { get => transform.position; }

	private GameObject cakeClone;
	private Rigidbody2D cakeCloneRigidbody;
	private CapsuleCollider2D cakeCloneCollider;

	private bool isReloading;

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
			cakeClone.transform.position = Vector2.Lerp(cakeClone.transform.position, transform.position, speed * Time.deltaTime);

			if (Vector3.Distance(cakeClone.transform.position, transform.position) <= 0.05f)
			{
				isReloading = false;
            }
		}
    }

    /// <summary>
    /// Создает торт и задает ему силу метания.
    /// </summary>
    /// <param name="force">Сила метания</param>
    public void Push(Vector2 force)
	{
		GameManager.StartSmoothDestroyer(cakeClone, objectPool, delayColor, stepColor); // Перезапускаем компонент для плавного удаления по времени

		cakeCloneCollider.transform.parent = null;

		cakeCloneRigidbody.isKinematic = false;
		cakeCloneRigidbody.AddForce(force, ForceMode2D.Impulse); // Устанавливаем скорость полета торта
		cakeCloneCollider.enabled = true; // Включаем коллайдер

		destroyer.AddToCollection(cakeClone); // Добавляем торт в коллекцию
		destroyer.DeleteOldObject(); // Удаляем старый торт

		ReloadCake(); // Перезаряжаем пушку
	}

	/// <summary>
	/// Перезаряжает пушку тортом.
	/// </summary>
	private void ReloadCake()
    {
		cakeClone = objectPool.GetObject(cakePrefab, reloadPoint.position, transform.rotation, transform); // Создаем новый торт
		cakeCloneRigidbody = cakeClone.GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
		cakeCloneRigidbody.isKinematic = true;

		cakeCloneCollider = cakeClone.GetComponent<CapsuleCollider2D>();
		cakeCloneCollider.enabled = false;

		isReloading = true;
	}
}
