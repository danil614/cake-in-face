using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	Camera cam;

	public Cake cake;
	public Hero hero;
	public Trajectory trajectory;
	[SerializeField] float pushForce = 4f;

	bool isDragging = false;
	
	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;

	void Start()
	{
		cam = Camera.main;
		cake.DesactivateRb();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isDragging = true;
			OnDragStart();
		}
		if (Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			OnDragEnd();
		}

		if (isDragging)
		{
			OnDrag();
		}
	}

	void OnDragStart()
	{
		cake.DesactivateRb();
		startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

		trajectory.Show();
	}
	void OnDragEnd()
	{
		cake.ActivateRb();
		cake.Push(force);
		trajectory.Hide();
	}

	void OnDrag()
	{
		endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
		distance = Vector2.Distance(startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		//just for debug
		Debug.DrawLine(startPoint, endPoint);


		trajectory.UpdateDots(cake.Position, force);
	}
}
