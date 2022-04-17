using UnityEngine;

public class CakeBreaking : MonoBehaviour
{
    [SerializeField] private GameObject splashes; // Эффект брызг
    [SerializeField] private GameObject[] splats; // Разные формы пятен
    [SerializeField][Header("Пул объектов")] private ObjectPool objectPool;
    [SerializeField][Header("Скорость угасания пятна")] private float stepColor;
    [SerializeField][Header("Задержка удаления пятна")] private float delayColor;
    [SerializeField][Header("Скорость для разрушения")] private float speedDestruction;

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

    private bool IsBreak(Vector2 velocity)
    {
        return velocity.magnitude > speedDestruction;
    }

    private void CreateSplat(Vector2 collisionPoint)
    {
        int numberSplat = Random.Range(0, 4); // Генерация для выбора формы пятна
        GameObject prefab = splats[numberSplat];

        float shift = Random.Range(5, 14) / 10; // Сдвиг пятна
        Vector3 splatPosition = new Vector3(collisionPoint.x + shift, collisionPoint.y, 0);
        GameObject splat = objectPool.GetObject(prefab, splatPosition, Quaternion.identity, transform); // Создание пятна на поваре
        GameManager.StartSmoothDestroyer(splat, objectPool, delayColor, stepColor); // Перезапускаем компонент для плавного удаления по времени
    }

    private void CreateSplashes(Vector2 collisionPoint)
    {
        Vector3 splashesPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0);
        GameObject splashesClone = objectPool.GetObject(splashes, splashesPosition, Quaternion.identity, null);
        GameManager.StartParticleSystemManager(splashesClone, objectPool); // Перезапускаем компонент для активации и деактивации системы частиц
    }
}
