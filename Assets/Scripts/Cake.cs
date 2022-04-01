using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
	[HideInInspector]
	public Vector3 Position { get => transform.position; }

	[SerializeField]
	private GameObject cakePrefab;

	[SerializeField]
	private int allowedNumberCakes; // Допустимое количество тортов на сцене

	private List<GameObject> cakesOnScene; // Коллекция для хранения тортов на сцене

    private void Start()
    {
		cakesOnScene = new List<GameObject>(); // Инициализируем коллекцию
	}

    /// <summary>
    /// Создает торт и задает ему силу метания.
    /// </summary>
    /// <param name="force">Сила метания</param>
    public void Push(Vector2 force)
	{
		GameObject cakeClone = Instantiate(cakePrefab, transform.position, Quaternion.identity); // Создаем новый торт
		cakeClone.transform.rotation = transform.rotation; // Изменеям угол вращения

		Rigidbody2D body2d = cakeClone.GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
		body2d.AddForce(force, ForceMode2D.Impulse); // Устанавливаем скорость полета торта
		
		cakesOnScene.Add(cakeClone); // Добавляем в список тортов
		DeleteOldCake(); // Удаляем старый торт
	}

	/// <summary>
	/// Удаляет объекты со сцены при превышении допустимого количества.
	/// </summary>
	public void DeleteOldCake()
    {
		if (cakesOnScene != null && cakesOnScene.Count > allowedNumberCakes)
        {
			Destroy(cakesOnScene[0]); // Удаляем со сцены самый старый торт
			cakesOnScene.RemoveAt(0); // Удаляем из списка
        }
    }
}
