using UnityEngine;

public class CakeBreaking : MonoBehaviour
{
    [SerializeField] private GameObject splashes; // Эффект брызг
    [SerializeField] private GameObject[] splats; // Разные формы пятен
    [SerializeField][Header("Пул объектов")] private ObjectPool objectPool;
    [SerializeField][Header("Скорость угасания пятна")] private float stepColor;
    [SerializeField][Header("Задержка удаления пятна")] private float delayColor;
    [SerializeField][Header("Скорость для разрушения")] private float speedDestruction;
    [SerializeField] private Collider2D[] collidersCook; //Группа коллайдеров частей тела
    [SerializeField] private float MinShift; //Минимально возможное смещение по координате x
    [SerializeField] private float MaxShift; //Маскимально возможное смещение по координате x
    

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

    /// <summary>
    /// Создает пятно
    /// </summary>
    /// <param name="collisionPoint"></param>

    private void CreateSplat(Vector2 collisionPoint)
    {
        float shift = Random.Range(MinShift, MaxShift); //Генерация смещения
        
        Vector3 checkPosition = new Vector3(collisionPoint.x + shift, collisionPoint.y, 0); //Смещаем на shift
        Vector3 splatPosition = new Vector3(collisionPoint.x, collisionPoint.y, 0); //Запоминаем начальное положение пятна
        int numberSplat = Random.Range(0, splats.Length); // Генерация для выбора формы пятна       
        GameObject prefab = splats[numberSplat];
        Transform currentTransform = transform; //Присваивание просто чтобы среда не ругалась
        foreach(Collider2D cookPart in collidersCook) //Перебираем коллайдеры частей тела которые находятся в данном массиве
        {
            currentTransform = cookPart.transform;
            if (cookPart.OverlapPoint(checkPosition)) //Проверяет попали ли координаты позиции на коллайдер
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
