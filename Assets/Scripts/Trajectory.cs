using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
	[SerializeField] int dotsNumber; // Количество точек в траектории
	[SerializeField] GameObject dotsParent; // Группировка всех точек
	[SerializeField] GameObject dotPrefab; // Объект - точка
	[SerializeField] float dotSpacing; // Расстояние между точками
	[SerializeField][Range(0.01f, 0.3f)] float dotMinScale; // Минимальный размер точек
	[SerializeField][Range(0.1f, 1f)] float dotMaxScale; // Максимальный размер точек

	Transform[] dotsList; // Массив точек для траектории

	void Start()
	{
		Hide(); // Спрятать траекторию
		PrepareDots();
	}

	/// <summary>
	/// Создает точки для траектории и рассчитывает их размер
	/// </summary>
	void PrepareDots()
	{
		dotsList = new Transform[dotsNumber];
		dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

		float scale = dotMaxScale;
		float scaleFactor = scale / dotsNumber;

		for (int i = 0; i < dotsNumber; i++)
		{
			dotsList[i] = Instantiate(dotPrefab, null).transform;
			dotsList[i].parent = dotsParent.transform;

			dotsList[i].localScale = Vector3.one * scale;
			
			if (scale > dotMinScale)
			{
				scale -= scaleFactor;
			}
		}
	}

	/// <summary>
	/// Устанавливает позиции всех точек траектории
	/// </summary>
	/// <param name="startPosition">Начальная позиция</param>
	/// <param name="forceApplied">Приложенная сила</param>
	public void UpdateDots(Vector3 startPosition, Vector2 forceApplied)
	{
		Vector2 position;
		float timeStamp = dotSpacing;

		for (int i = 0; i < dotsNumber; i++)
		{
			position.x = (startPosition.x + forceApplied.x * timeStamp);
			position.y = (startPosition.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

			dotsList[i].position = position;
			timeStamp += dotSpacing;
		}
	}

	/// <summary>
	/// Показывает траекторию
	/// </summary>
	public void Show()
	{
		dotsParent.SetActive(true);
	}

	/// <summary>
	/// Прячет траекторию
	/// </summary>
	public void Hide()
	{
		dotsParent.SetActive(false);
	}
}
