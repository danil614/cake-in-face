using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Cake")
		{
			//anim.SetBool("isContact",true);
		}
	}
}
