using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
	[SerializeField] GameObject cake;
	//[HideInInspector] public Rigidbody2D body2d;
	//[HideInInspector] public BoxCollider2D collider2d;

	[HideInInspector] public Vector3 Position { get { return transform.position; } }

	void Awake()
	{
		//body2d = GetComponent<Rigidbody2D>();
		//collider2d = GetComponent<BoxCollider2D>();
	}

	public void Push(Vector2 force)
	{
		//body2d.AddForce(force, ForceMode2D.Impulse);
		GameObject cakeClone = Instantiate(cake, transform.position, Quaternion.identity);
		Rigidbody2D body2d = cakeClone.GetComponent<Rigidbody2D>();
		body2d.AddForce(force, ForceMode2D.Impulse);
	}
	public void ActivateRb()
	{
		//body2d.isKinematic = false; // При начале движения становится Dynamic
	}

	public void DesactivateRb()
	{
		//body2d.isKinematic = true;
		//body2d.angularVelocity = 0f;
	}
}
