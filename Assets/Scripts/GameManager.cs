using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Camera mainCamera;
	public Cake cake;
	public Trajectory trajectory;
	[SerializeField] float pushForce; // Ñèëà íàæàòèÿ

	bool isClicking = false; // Íàæàòèå íà ýêðàí
	Vector2 force; // Ñèëà ìåòàíèÿ òîðòà

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isClicking = true;
			trajectory.Show(); // Ïîêàçûâàåì òðàåêòîðèþ
		}

		if (Input.GetMouseButtonUp(0))
		{
			isClicking = false;
			cake.Push(force); // Ìåòàåì òîðò
			trajectory.Hide(); // Ïðÿ÷åì òðàåêòîðèþ
		}

		if (isClicking)
		{
			OnClick(); // Îáíîâëåíèå òðàåêòîðèè ïðè íàæàòèè
		}
	}

	/// <summary>
	/// Ïîñòðîåíèå òðàåêòîðèè ïðè íàæàòèè
	/// </summary>
	void OnClick()
	{
		// Òî÷êè íà÷àëà è êîíöà íàïðàâëÿþùåé ëèíèè òðàåêòîðèè
		Vector2 startPoint = cake.Position;
		Vector2 endPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		// Íàïðàâëÿþùàÿ ëèíèÿ äëÿ îòëàäêè
		Debug.DrawLine(startPoint, endPoint);

		float distance = Vector2.Distance(endPoint, startPoint);
		Vector2 direction = (endPoint - startPoint).normalized;

		force = distance * pushForce * direction;

		// Ðàññòàâèòü âñå òî÷êè ïî òðàåêòîðèè ïîëåòà
		trajectory.UpdateDots(cake.Position, force);
	}
}
