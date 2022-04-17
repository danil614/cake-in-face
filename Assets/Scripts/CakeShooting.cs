using UnityEngine;

public class CakeShooting : MonoBehaviour
{
	[SerializeField] private GameObject cakePrefab; // Префаб торта
	[SerializeField][Header("Удаление торта по количеству")] private Destroyer destroyer;
	[SerializeField][Header("Пул объектов")] private ObjectPool objectPool;
	[SerializeField][Header("Скорость угасания торта")] private float stepColor;
	[SerializeField][Header("Задержка удаления торта")] private float delayColor;

	[HideInInspector] public Vector3 Position { get => transform.position; }

	/// <summary>
	/// Создает торт и задает ему силу метания.
	/// </summary>
	/// <param name="force">Сила метания</param>
	public void Push(Vector2 force)
	{
		GameObject cakeClone = objectPool.GetObject(cakePrefab, transform.position, transform.rotation, null); // Создаем новый торт
		GameManager.StartSmoothDestroyer(cakeClone, objectPool, delayColor, stepColor); // Перезапускаем компонент для плавного удаления по времени

		Rigidbody2D body2d = cakeClone.GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
		body2d.AddForce(force, ForceMode2D.Impulse); // Устанавливаем скорость полета торта

		destroyer.AddToCollection(cakeClone); // Добавляем торт в коллекцию
		destroyer.DeleteOldObject(); // Удаляем старый торт
	}
}
