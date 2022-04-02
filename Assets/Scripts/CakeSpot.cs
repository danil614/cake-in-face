using UnityEngine;

public class CakeSpot : MonoBehaviour
{
    public GameObject particle; // Эффект брызг
    [SerializeField] private GameObject[] slpats = new GameObject[5]; // Разные формы пятен

	Animator animator;

	private void Start()
	{
        try
        {
            animator = GetComponent<Animator>();
        }
        catch (MissingComponentException)
        {
            animator = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
        {
            Destroy(collision.gameObject); // Уничтожение объекта

            if (collision.contactCount > 0)
            {
                Vector2 collisionPoint = collision.GetContact(0).point;

                int numberSplat = Random.Range(0, 4); // Генерация для выбора формы пятна
                float shift = Random.Range(5, 14) / 10; // Сдвиг пятна
                GameObject splat = Instantiate(slpats[numberSplat], new Vector3(collisionPoint.x + shift, collisionPoint.y, 0), Quaternion.identity); // Создание пятна на поваре
                splat.transform.parent = transform; // Удочерение пятна

                Instantiate(particle, new Vector3(collisionPoint.x, collisionPoint.y, 0), Quaternion.identity); // Создание эффекта брызг
            }

            if (animator != null)
            {
                animator.SetTrigger("Angry"); // Проигрывание анимации
            }
        }    
    }
}
