using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
	[HideInInspector] public Vector3 Position { get => transform.position; }
	[SerializeField] private GameObject cake;

	/// <summary>
	/// Создает торт и задает ему силу метания
	/// </summary>
	/// <param name="force">Сила метания</param>
	public void Push(Vector2 force)
	{
		// Quaternion.identity - торт летит прямо?
		GameObject cakeClone = Instantiate(cake, transform.position, Quaternion.identity); // Создаем новый торт
		Rigidbody2D body2d = cakeClone.GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
		body2d.AddForce(force, ForceMode2D.Impulse); // Устанавливаем скорость полета торта
	}
}
