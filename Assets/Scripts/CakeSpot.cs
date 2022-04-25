using UnityEngine;

public class CakeSpot : MonoBehaviour
{
    public GameObject particle; // Эффект брызг
    [SerializeField] private GameObject[] slpats = new GameObject[5]; // Разные формы пятен

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float shift;
        if (collision.gameObject.CompareTag("Cake"))
        {
            Destroy(collision.gameObject); // Уничтожение объекта
            
            if (collision.contactCount > 0)
            {
                if(gameObject.tag == "LeftHand")
                {
                    shift = -0.1f;
                }
                else
                {
                    shift = Random.Range(5, 14) / 10; // Сдвиг пятна
                }
                Vector2 collisionPoint = collision.GetContact(0).point;
                int numberSplat = Random.Range(0, 4); // Генерация для выбора формы пятна
                
                GameObject splat = Instantiate(slpats[numberSplat], new Vector3(collisionPoint.x + shift, collisionPoint.y, 0), Quaternion.Euler(0,90*(Random.Range(0,30)/10),0)); // Создание пятна на поваре
                splat.transform.parent = transform; // Удочерение пятна

                Instantiate(particle, new Vector3(collisionPoint.x, collisionPoint.y, 0), Quaternion.identity); // Создание эффекта брызг
            }
        }    
    }
}
