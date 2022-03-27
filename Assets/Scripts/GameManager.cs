using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private Cake cake;

	[SerializeField]
	private Trajectory trajectory;

	[SerializeField]
	private Transform turningPoint; // Точка поворота

	[SerializeField]
	private float pushForce; // Сила нажатия

	[SerializeField]
	private float startPushForce; // Стартовая сила нажатия

	private bool isClicking = false; // Нажатие на экран
	private Vector2 force; // Сила метания торта
	private float time; // Подсчет времени для роста траектории

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isClicking = true;
			SetStartPushForce(); // Обнуляем время
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
	/// Устанавливает стартовую силу полета торта в зависимости от расстояния до нажатия.
	/// </summary>
	private void SetStartPushForce()
    {
		Vector2 startPoint = turningPoint.position;
		Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		time = Vector2.Distance(endPoint, startPoint) * startPushForce; // Устанваливаем стартовое время
	}

	/// <summary>
	/// Построение траектории при нажатии.
	/// </summary>
	private void OnClick()
	{
		// Точки начала и конца направляющей линии траектории
		Vector2 startPoint = turningPoint.position;
		Vector2 endPoint = cake.Position;

		// Направляющий вектор длиной 1
		Vector2 direction = (endPoint - startPoint).normalized;

		// Сила метания торта
		force = time * pushForce * direction;

		// Расставить все точки по траектории полета
		trajectory.UpdateDots(cake.Position, force);
	}
}