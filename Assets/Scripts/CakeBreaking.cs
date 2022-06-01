using UnityEngine;

public class CakeBreaking : MonoBehaviour
{
    [SerializeField] [Header("Эффект брызг")]
    private GameObject splashes;

    [SerializeField] [Header("Разные формы пятен")]
    private GameObject[] splats;

    [SerializeField] [Header("Пул объектов")]
    private ObjectPool objectPool;

    [SerializeField] [Header("Скорость разрушения торта")]
    private float speedDestruction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
            if (IsBreak(collision.relativeVelocity))
            {
                objectPool.ReturnObject(collision.gameObject); // Убираем торт в пул объектов

                if (collision.contactCount > 0)
                {
                    var collisionPoint = collision.GetContact(0).point; // Точка касания
                    CreateSplat(collisionPoint);
                    CreateSplashes(collisionPoint);
                }
            }
    }

    private bool IsBreak(Vector2 velocity)
    {
        return velocity.magnitude > speedDestruction;
    }

    private void CreateSplat(Vector2 collisionPoint)
    {
        var numberSplat = Random.Range(0, 4); // Генерация для выбора формы пятна
        var prefab = splats[numberSplat];

        var shift = Random.Range(0.5f, 4); // Сдвиг пятна
        var splatPosition = new Vector3(collisionPoint.x + shift, collisionPoint.y, 0);
        var currentSplat =
            objectPool.GetObject(prefab, splatPosition, Quaternion.identity, transform); // Создание пятна на поваре
        GameManager.StartSmoothDestroyer(currentSplat, true); // Запускаем плавное удаление пятна по времени
    }

    private void CreateSplashes(Vector2 collisionPoint)
    {
        var splashesPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0);
        objectPool.GetObject(splashes, splashesPosition, Quaternion.identity, null);
    }
}