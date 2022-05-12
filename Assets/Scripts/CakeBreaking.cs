using UnityEngine;
using System.Collections;

public class CakeBreaking : MonoBehaviour
{
    [SerializeField] private GameObject splashes; // Эффект брызг
    [SerializeField] private GameObject[] splats; // Разные формы пятен
    [SerializeField][Header("Пул объектов")] private ObjectPool objectPool;
    [SerializeField][Header("Скорость угасания пятна")] private float stepColor;
    [SerializeField][Header("Задержка удаления пятна")] private float delayColor;
    [SerializeField][Header("Скорость для разрушения")] private float speedDestruction;
    [SerializeField] private Collider2D collider2D;
    //[SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float MinShift = 0.0f;
    [SerializeField] private float MaxShift = 2.5f;
    

    //private void Start()
    //{
    //    //if (gameObject.name == "Head") StartCoroutine(PrintTouch(circleCollider));
    //}

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
        {
            if (IsBreak(collision.relativeVelocity))
            {
                GameManager.StopSmoothDestroyer(collision.gameObject); // Останавливаем компонент плавного удаления
                objectPool.ReturnObject(collision.gameObject); // Убираем торт в пул объектов

                if (collision.contactCount > 0)
                {
                    Vector2 collisionPoint = collision.GetContact(0).point; // Точка касания
                    CreateSplat(collisionPoint);
                    CreateSplashes(collisionPoint);
                }
            }
        }
    }
    //static bool flag = false;
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.IsTouching(capsuleCollider2D))
    //    {
    //        flag = true;
    //    }

    //}
    //private void FixedUpdate()
    //{

    //}
    //private void MovingSplats()
    //{
    //    float shift;
    //    shift = Random.Range(0.0f, 3.0f); // Сдвиг пятна
    //    for (float i = 0; i < shift; i += 0.1f)
    //    {
    //        if(flag == true)
    //        {

    //        }
    //    }
    //}

    private bool IsBreak(Vector2 velocity)
    {
        return velocity.magnitude > speedDestruction;
    }
    IEnumerator PrintTouch(Collider2D _circleCollider, GameObject splat)
    {
        float shift = Random.Range(MinShift, MaxShift);
        splat.GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.02f);
        if (CheckTouch(_circleCollider))
        {
            splat.transform.Translate(shift, 0, 0);
            splat.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
            
        yield return new WaitForSeconds(0.02f);
        if (!CheckTouch(_circleCollider))
        {
            
            for (float i = 0; i < shift; i += 0.1f)
            {
                yield return new WaitForSeconds(0.02f);
                if (CheckTouch(_circleCollider))
                {
                    //splat.GetComponent<SpriteRenderer>().enabled = true;
                    break;
                }
                else
                    splat.transform.Translate(-0.1f, 0, 0);
            }
            splat.GetComponentInChildren<SpriteRenderer>().enabled = true;
            //do
            //{
            //    yield return new WaitForFixedUpdate();
            //    yield return new WaitForFixedUpdate();

            //} while (!CheckTouch(_circleCollider));
        }
        //CheckTouch(_circleCollider, "--CORRRR");



    }
    private bool CheckTouch(Collider2D collision, string text = "")
    {
        if (collider2D.IsTouching(collision))
        {
            Debug.Log(text + " Вау оно работает");
            return true;
        }
            
        else
        {
            Debug.Log(text + " НЕЕ работает");
            return false;
        }
    }
    private void CreateSplat(Vector2 collisionPoint)
    {
        int numberSplat = Random.Range(0, 2); // Генерация для выбора формы пятна
        GameObject prefab = splats[numberSplat];
        //float shift = Random.Range(0.0f, 3.0f); // Сдвиг пятна
        Vector3 splatPosition = new Vector3(collisionPoint.x+0.5f, collisionPoint.y, 0);
        GameObject splat = objectPool.GetObject(prefab, splatPosition, Quaternion.Euler(0,0,0), transform); // Создание пятна на поваре

        Collider2D splatCol = splat.GetComponent<Collider2D>();
        if (splatCol)
            Debug.Log("Все нормуль");
        else
            Debug.Log("Его нет");

        if (gameObject.name == "Head") StartCoroutine(PrintTouch(splatCol,splat));

        GameManager.StartSmoothDestroyer(splat, objectPool, delayColor, stepColor); // Перезапускаем компонент для плавного удаления по времени
    }

    private void CreateSplashes(Vector2 collisionPoint)
    {
        Vector3 splashesPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0);
        GameObject splashesClone = objectPool.GetObject(splashes, splashesPosition, Quaternion.identity, null);
        GameManager.StartParticleSystemManager(splashesClone, objectPool); // Перезапускаем компонент для активации и деактивации системы частиц
    }
}
