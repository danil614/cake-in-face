using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakePiece : MonoBehaviour
{
	public GameObject piece;
	public GameObject hero;
	/// <summary>
	/// При столкновении уничтожается объект
	/// </summary>
	/// <param name="collision"></param>
	private void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.tag == "Cake")
		{
			Destroy(collision.gameObject);
			foreach (ContactPoint2D collisionContact in collision.contacts)
			{
				Vector2 collisionPoint = collisionContact.point;
				Transform pieces;
				pieces = Instantiate(piece, new Vector3(collisionPoint.x+0.1f, collisionPoint.y, 0), Quaternion.identity).transform;
				pieces.parent = hero.transform; // Удочеряем 
				break;
			}

		}
	}


}
