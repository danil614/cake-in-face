using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeBreaking : MonoBehaviour
{
    [SerializeField] private GameObject splashes; // Эффект брызг
    [SerializeField] private GameObject[] splats; // Разные формы пятен
    [SerializeField][Header("Пул объектов")] private ObjectPool objectPool;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
        {
            objectPool.ReturnObject(collision.gameObject); // Убираем в пул объектов

            if (collision.contactCount > 0)
            {
                Vector2 collisionPoint = collision.GetContact(0).point; // Точка касания
                CreateSplat(collisionPoint);
                CreateSplashes(collisionPoint);
            }
        }
    }

    private void CreateSplat(Vector2 collisionPoint)
    {
        int numberSplat = Random.Range(0, 4); // Генерация для выбора формы пятна
        GameObject prefab = splats[numberSplat];

        float shift = Random.Range(5, 14) / 10; // Сдвиг пятна
        Vector3 splatPosition = new Vector3(collisionPoint.x + shift, collisionPoint.y, 0);
        objectPool.GetObject(prefab, splatPosition, Quaternion.identity, transform); // Создание пятна на поваре
    }

    private void CreateSplashes(Vector2 collisionPoint)
    {
        Vector3 splashesPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0);
        objectPool.GetObject(splashes, splashesPosition, Quaternion.identity, null);
    }
}
