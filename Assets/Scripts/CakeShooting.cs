using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeShooting : MonoBehaviour
{
	[SerializeField] private GameObject cakePrefab; // Префаб торта
	[SerializeField] private int allowedNumberCakes; // Допустимое количество тортов на сцене
	[SerializeField][Header("Пул объектов")] private ObjectPool objectPool;

	[HideInInspector] public Vector3 Position { get => transform.position; }
	private Queue<GameObject> cakesOnScene; // Коллекция для хранения тортов на сцене

	private void Start()
	{
		cakesOnScene = new Queue<GameObject>(); // Инициализируем коллекцию
	}

	/// <summary>
	/// Создает торт и задает ему силу метания.
	/// </summary>
	/// <param name="force">Сила метания</param>
	public void Push(Vector2 force)
	{
		GameObject cakeClone = objectPool.GetObject(cakePrefab, transform.position, transform.rotation, null); // Создаем новый торт

		Rigidbody2D body2d = cakeClone.GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
		body2d.AddForce(force, ForceMode2D.Impulse); // Устанавливаем скорость полета торта

		cakesOnScene.Enqueue(cakeClone); // Добавляем в список тортов
		DeleteOldCake(); // Удаляем старый торт
	}

	/// <summary>
	/// Удаляет объекты со сцены при превышении допустимого количества.
	/// </summary>
	public void DeleteOldCake()
	{
		if (cakesOnScene != null && cakesOnScene.Count > allowedNumberCakes)
		{
			GameObject oldCake = cakesOnScene.Dequeue(); // Получаем торт и удаляем из очереди
			objectPool.ReturnObject(oldCake); // Удаляем со сцены самый старый торт
		}
	}
}
