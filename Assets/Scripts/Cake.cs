using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D body2d;
	//[HideInInspector] public BoxCollider2D collider2d;

	[HideInInspector] public Vector3 Position { get { return transform.position; } }

	void Awake()
	{
		body2d = GetComponent<Rigidbody2D>();
		//collider2d = GetComponent<BoxCollider2D>();
	}

	public void Push(Vector2 force)
	{
		body2d.AddForce(force, ForceMode2D.Impulse);
	}

	public void DesactivateRb()
	{
		body2d.angularVelocity = 0f;
	}
}
