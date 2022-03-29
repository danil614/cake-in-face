using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSpot : MonoBehaviour
{
    public GameObject prefabSpot; // Пятно
    public GameObject particle; // Эффект брызг

    private List<GameObject> particles = new List<GameObject>(); // Лист эффектов для удаления
    private List<GameObject> spots = new List<GameObject>();

    [SerializeField]
    private float stepColor;

    [SerializeField]
    private float delayColor;

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
                float s = Random.Range(5, 14) / 10; // rnd.Next(5, 14) / 10; // Возвращает случайное целое число между min [вкл] и max [искл] (Read Only).

                Vector2 collisionPoint = collision.GetContact(0).point;
                GameObject spot = Instantiate(prefabSpot, new Vector3(collisionPoint.x + s, collisionPoint.y, 0), Quaternion.identity);
                spot.transform.parent = transform;
                spots.Add(spot);

                GameObject particleClone = Instantiate(particle, new Vector3(collision.transform.position.x, collision.transform.position.y, 0), Quaternion.identity);
                particles.Add(particleClone);
            }
            //anim.SetBool("isContact",true);
            anim.SetTrigger("trigger");
        }
        //anim.SetBool("isContact", false);
        DeleteParticle();
        StartCoroutine(SlowDelete());
    }
    public void DeleteParticle() // В настройках есть удаление
    {
        if (particles.Count > 1)
        {
            Destroy(particles[0]); // Удаляем со сцены
            particles.RemoveAt(0); // Удаляем из списка
        }
    }
    private IEnumerator SlowDelete()
    {
        Debug.Log("Start SlowDelete");

        if (spots.Count > 2)
        {
            Color color = spots[0].GetComponent<SpriteRenderer>().color;
            do
            {
                color.a -= stepColor;
                spots[0].GetComponent<SpriteRenderer>().color = color;
                yield return new WaitForSeconds(delayColor);
                Debug.Log($"Цвет: {spots[0].GetComponent<SpriteRenderer>().color}");
            }
            while (color.a > 0);

            Destroy(spots[0]);
            spots.RemoveAt(0);
        }
    }
}
