using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Camera mainCamera;
	public Cake cake;
	public Trajectory trajectory;
	[SerializeField] float pushForce; // Сила нажатия

	bool isClicking = false; // Нажатие на экран
	Vector2 force; // Сила метания торта

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isClicking = true;
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
		Vector2 endPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		// Направляющая линия для отладки
		Debug.DrawLine(startPoint, endPoint);

		float distance = Vector2.Distance(endPoint, startPoint);
		Vector2 direction = (endPoint - startPoint).normalized;

		force = distance * pushForce * direction;

		// Расставить все точки по траектории полета
		trajectory.UpdateDots(cake.Position, force);
	}
}
