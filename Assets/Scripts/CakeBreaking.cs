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

    [SerializeField] private Collider2D[] collidersCook; // Группа коллайдеров частей тела
    [SerializeField] private float minShift; // Минимально возможное смещение по координате x
    [SerializeField] private float maxShift; // Маскимально возможное смещение по координате x


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
        var shift = Random.Range(minShift, maxShift); // Генерация смещения
        var checkPosition = new Vector3(collisionPoint.x + shift, collisionPoint.y, 0); // Смещаем на shift
        var splatPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0); // Запоминаем начальное положение пятна
        var numberSplat = Random.Range(0, splats.Length); // Генерация для выбора формы пятна       
        var prefab = splats[numberSplat];
        var currentTransform = transform; // Присваивание просто чтобы среда не ругалась
        foreach (var cookPart in collidersCook) // Перебираем коллайдеры частей тела которые находятся в данном массиве
        {
            currentTransform = cookPart.transform;
            if (cookPart.OverlapPoint(checkPosition)) // Проверяет попали ли координаты позиции на коллайдер
            {
                splatPosition =
                    new Vector3(collisionPoint.x + shift, collisionPoint.y,
                        0); // Если попало, то берем координаты со смещением
                break;
            }
        }
        // Создание пятна на поваре
        var currentSplat = objectPool.GetObject(prefab, splatPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)), currentTransform);
        GameManager.StartSmoothDestroyer(currentSplat, true); // Запускаем плавное удаление пятна по времени
    }

    private void CreateSplashes(Vector2 collisionPoint)
    {
        var splashesPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0);
        objectPool.GetObject(splashes, splashesPosition, Quaternion.identity, null);
    }
}