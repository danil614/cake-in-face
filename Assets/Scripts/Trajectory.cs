using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
	[SerializeField]
	int dotsNumber; // Количество точек в траектории

	[SerializeField]
	GameObject dotPrefab; // Объект - точка

	[SerializeField]
	float dotSpacing; // Расстояние между точками

	[SerializeField][Range(0.01f, 0.3f)]
	float dotMinScale; // Минимальный размер точек
	[SerializeField][Range(0.1f, 1f)]
	float dotMaxScale; // Максимальный размер точек

	Transform[] dotsList; // Массив точек для траектории

    void Start()
	{
		Hide(); // Прячем траекторию
		PrepareDots(); // Подготавливаем точки для траектории
	}

	/// <summary>
	/// Создает точки для траектории и рассчитывает их размер.
	/// </summary>
	void PrepareDots()
	{
		dotsList = new Transform[dotsNumber]; // Создаем массив точек траектории
		dotPrefab.transform.localScale = Vector3.one * dotMaxScale; // Устанавливаем максимальный размер точек

		float scale = dotMaxScale;
		float scaleFactor = scale / dotsNumber;

		for (int i = 0; i < dotsNumber; i++)
		{
			dotsList[i] = Instantiate(dotPrefab, null).transform; // Создаем точку
			dotsList[i].parent = transform; // Группируем точку

			dotsList[i].localScale = Vector3.one * scale; // Устанавливаем новый размер точки
			
			if (scale > dotMinScale)
			{
				scale -= scaleFactor; // Уменьшаем размер
			}
		}
	}

	/// <summary>
	/// Устанавливает позиции всех точек траектории по формуле.
	/// </summary>
	/// <param name="startPosition">Начальная позиция</param>
	/// <param name="forceApplied">Приложенная сила</param>
	public void UpdateDots(Vector3 startPosition, Vector2 forceApplied)
	{
		Vector2 position;
		float timeStamp = dotSpacing;

		for (int i = 0; i < dotsNumber; i++)
		{
			// x = x0 + V0 * t - уравнение равномерного движения
			position.x = startPosition.x + forceApplied.x * timeStamp;

			// y = y0 + V0 * t - g * t^2 / 2 - уравнение равноускоренного движения
			position.y = startPosition.y + forceApplied.y * timeStamp - Physics2D.gravity.magnitude * timeStamp * timeStamp / 2f;

			dotsList[i].position = position; // Применяем положение точки
			timeStamp += dotSpacing; // Увеличиваем время
		}
	}

	/// <summary>
	/// Показывает траекторию.
	/// </summary>
	public void Show()
	{
		gameObject.SetActive(true);
	}

	/// <summary>
	/// Прячет траекторию.
	/// </summary>
	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
