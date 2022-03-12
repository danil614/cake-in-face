using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
	[HideInInspector] public Vector3 Position { get => transform.position; }
	[SerializeField] private GameObject cake;
	[SerializeField] private int allowedNumberCakes; // Допустимое количество тортов на сцене
	private List<GameObject> objectsOnScene = new List<GameObject>();

	/// <summary>
	/// Создает торт и задает ему силу метания
	/// </summary>
	/// <param name="force">Сила метания</param>
	public void Push(Vector2 force)
	{
		GameObject cakeClone = Instantiate(cake, transform.position, Quaternion.identity); // Создаем новый торт
		cakeClone.transform.rotation = transform.rotation; // Изменеям угол вращения
		Rigidbody2D body2d = cakeClone.GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
		body2d.AddForce(force, ForceMode2D.Impulse); // Устанавливаем скорость полета торта
		objectsOnScene.Add(cakeClone); // Добавляем в список объектов
		DeleteObjects(); // Удаляем старые торты
	}

	/// <summary>
	/// Удаляет объекты со сцены при превышении допустимого количества
	/// </summary>
	public void DeleteObjects()
    {
		if (objectsOnScene.Count > allowedNumberCakes)
        {
			Destroy(objectsOnScene[0]); // Удаляем со сцены
			objectsOnScene.RemoveAt(0); // Удаляем из списка
        }
    }
}
