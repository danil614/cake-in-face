using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DestroyerByNumber : MonoBehaviour
{
	private LinkedList<GameObject> objectsOnScene; // Коллекция для хранения объектов на сцене

	[SerializeField][Header("Допустимое количество объектов на сцене")] private int allowedNumber;
	[SerializeField][Header("Пул объектов")] private ObjectPool objectPool;
	[SerializeField][Header("Задержка удаления объектов")] private float delay;

	private void Awake()
	{
		objectsOnScene = new LinkedList<GameObject>(); // Инициализируем коллекцию
	}

	private void Update()
	{
		StartCoroutine(DeleteOldObjectByNumber()); // Удаление тортов по количеству
	}

	private IEnumerator DeleteOldObjectByNumber()
	{
		yield return new WaitForSeconds(delay);
		DeleteOldObject(); // Удаляем самый старый торт
	}

	/// <summary>
	/// Добавляет объект в коллекцию.
	/// </summary>
	public void AddToCollection(GameObject gameObject)
    {
		if (objectsOnScene != null)
		{
			objectsOnScene.AddLast(gameObject); // Добавляем в конец списка
		}
	}

	/// <summary>
	/// Удаляет объект из коллекции.
	/// </summary>
	public void DeleteFromCollection(GameObject gameObject)
    {
		if (objectsOnScene != null && objectsOnScene.Count > 0)
		{
			objectsOnScene.Remove(gameObject); // Удаляем объект из списка
		}
	}

	/// <summary>
	/// Удаляет объекты со сцены при превышении допустимого количества.
	/// </summary>
	public void DeleteOldObject()
	{
		if (objectsOnScene != null && objectsOnScene.Count > allowedNumber)
		{
			GameObject oldObject = objectsOnScene.First.Value; // Получаем самый старый объект
			GameManager.StartSmoothDestroyer(oldObject); // Плавно удаляем со сцены
		}
	}
}
