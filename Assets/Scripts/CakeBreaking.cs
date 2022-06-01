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
    [SerializeField] private Collider2D[] collidersCook;
    //[SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float MinShift;
    [SerializeField] private float MaxShift;
    

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
    //IEnumerator PrintTouch(GameObject splat, Vector3 positionLocal)
    //{
        
    //    yield return new WaitForFixedUpdate();
    //    yield return new WaitForFixedUpdate();
    //    if (!CheckTouch(splatCol))
    //    {
    //        splat.transform.localPosition = positionLocal;
    //        //splat.GetComponent<SpriteRenderer>().enabled = false;
    //        //for (float i = 0; i < 2.5; i += 0.1f)
    //        //{
    //        //    //yield return new WaitForSeconds(0.004f);
    //        //    yield return new WaitForFixedUpdate();
    //        //    yield return new WaitForFixedUpdate();
    //        //    if (CheckTouch(splatCol))
    //        //    {
    //        //        //splat.GetComponent<SpriteRenderer>().enabled = true;
    //        //        break;
    //        //    }
    //        //    else
    //        //        splat.transform.Translate(-0.1f, 0, 0);
    //        //}
    //        //splat.GetComponent<SpriteRenderer>().enabled = true;
    //        //do
    //        //{
    //        //    yield return new WaitForFixedUpdate();
    //        //    yield return new WaitForFixedUpdate();
    //        //} while (!CheckTouch(_circleCollider));
    //    }
    //}
    //private bool CheckTouch(Collider2D collision, string text = "")
    //{
    //    if (colliderCook.IsTouching(collision))
    //    {
    //        Debug.Log(text + " Вау оно работает");
    //        return true;
    //    }
            
    //    else
    //    {
    //        Debug.Log(text + " НЕЕ работает");
    //        return false;
    //    }
    //}
    private void CreateSplat(Vector2 collisionPoint)
    {
        float shift = Random.Range(MinShift, MaxShift);
        
        Vector3 checkPosition = new Vector3(collisionPoint.x + shift, collisionPoint.y, 0); //Смещаем на shift
        Vector3 splatPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0); //Запоминаем начальное положение пятна
        int numberSplat = Random.Range(0, splats.Length); // Генерация для выбора формы пятна       
        GameObject prefab = splats[numberSplat];
        Transform currentTransform = transform; 
        foreach(Collider2D cookPart in collidersCook) //Перебираем коллайдеры частей тела которые находятся в данном массиве
        {
            currentTransform = cookPart.transform;
            if (cookPart.OverlapPoint(checkPosition)) //Проверяет попало ли пятно на повара
            {
                splatPosition = new Vector3(collisionPoint.x + shift, collisionPoint.y, 0); //Если попало, то берем координаты со смещением
                break;
            }          
        }       
        GameObject splat = objectPool.GetObject(prefab, splatPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)), currentTransform); // Создание пятна на поваре

        GameManager.StartSmoothDestroyer(splat, objectPool, delayColor, stepColor); // Перезапускаем компонент для плавного удаления по времени
    }

    private void CreateSplashes(Vector2 collisionPoint)
    {
        Vector3 splashesPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0);
        GameObject splashesClone = objectPool.GetObject(splashes, splashesPosition, Quaternion.identity, null);
        GameManager.StartParticleSystemManager(splashesClone, objectPool); // Перезапускаем компонент для активации и деактивации системы частиц
    }
}
