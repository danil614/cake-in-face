using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	private ConcurrentQueue<GameObject> objectsOnScene; // Коллекция для хранения объектов на сцене

	[SerializeField][Header("Допустимое количество объектов на сцене")] private int allowedNumberCakes;
	[SerializeField][Header("Пул объектов")] private ObjectPool objectPool;

	private void Awake()
	{
		objectsOnScene = new ConcurrentQueue<GameObject>(); // Инициализируем коллекцию
	}

	public void AddToCollection(GameObject gameObject)
    {
		if (objectsOnScene != null)
		{
			objectsOnScene.Enqueue(gameObject); // Добавляем в очередь
		}
	}

	public void DeleteFromCollection()
    {
		if (objectsOnScene != null && objectsOnScene.Count > 0)
		{
			objectsOnScene.TryDequeue(out _); // Удаляем из очереди
		}
	}

	/// <summary>
	/// Удаляет объекты со сцены при превышении допустимого количества.
	/// </summary>
	public void DeleteOldObject()
	{
		if (objectsOnScene != null && objectsOnScene.Count > allowedNumberCakes)
		{
			if (objectsOnScene.TryPeek(out GameObject oldObject)) // Получаем самый старый объект объект
			{
				GameManager.StartSmoothDestroyer(oldObject, objectPool, 0, 1); // Плавно удаляем со сцены
			}
		}
	}
}
