using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Camera mainCamera;

	[SerializeField]
	private Cake cake;

	[SerializeField]
	private Trajectory trajectory;

	[SerializeField]
	private Transform trajectoryEndPoint;

	[SerializeField]
	private float pushForce; // Сила нажатия

	[SerializeField]
	private float startTime; // Стартовое время

	private bool isClicking = false; // Нажатие на экран
	private Vector2 force; // Сила метания торта
	private float time; // Подсчет времени для роста траектории

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isClicking = true;
			time = startTime; // Обнуляем время
			trajectory.Show(); // Показываем траекторию
		}

		if (Input.GetMouseButtonUp(0))
		{
			isClicking = false;
			cake.Push(force); // Метаем торт
			trajectory.Hide(); // Прячем траекторию
		}

		if (isClicking)
		{
			time += Time.deltaTime; // Считаем время нажатия
			OnClick(); // Обновление траектории при нажатии
		}
	}

	/// <summary>
	/// Построение траектории при нажатии
	/// </summary>
	void OnClick()
	{
		// Точки начала и конца направляющей линии траектории
		Vector2 startPoint = cake.Position;
		Vector2 endPoint = trajectoryEndPoint.position;

		// Направляющая линия для отладки
		Debug.DrawLine(startPoint, endPoint);

		float distance = Vector2.Distance(endPoint, startPoint);
		Vector2 direction = (endPoint - startPoint).normalized;

		force = distance * pushForce * time * direction;

		// Расставить все точки по траектории полета
		trajectory.UpdateDots(cake.Position, force);
	}
}