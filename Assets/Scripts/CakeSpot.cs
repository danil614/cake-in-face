using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSpot : MonoBehaviour
{
    //public GameObject prefabSpot; // Пятно
    //public GameObject prefabSpot2;
    //public GameObject prefabSpot3;
    //public GameObject prefabSpot4;
    //public GameObject prefabSpot5;
    public GameObject particle; // Эффект брызг
    private List<GameObject> spots = new List<GameObject>();
    [SerializeField] private GameObject[] slpats = new GameObject[5]; 


//    System.Random rnd = new System.Random(); // Для рандомного смещения пятен
	Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
    }
    /// <summary>
    /// При столкновении уничтожается объект
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
        {
            Destroy(collision.gameObject);

            if (collision.contactCount > 0)
            {
                int i = Random.Range(0, 4);
                float s = Random.Range(5, 14) / 10; // rnd.Next(5, 14) / 10; // Возвращает случайное целое число между min [вкл] и max [искл] (Read Only).
                Vector2 collisionPoint = collision.GetContact(0).point;
                GameObject spot = Instantiate(slpats[i], new Vector3(collisionPoint.x + s, collisionPoint.y, 0), Quaternion.identity);
                spot.transform.parent = transform;
                spots.Add(spot);
                GameObject particleClone = Instantiate(particle, new Vector3(collision.transform.position.x, collision.transform.position.y, 0), Quaternion.identity);
            }
            //anim.SetBool("isContact",true);
            anim.SetTrigger("trigger");
        }
        //anim.SetBool("isContact", false);
        
        
    }

}
