using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField][Header("Объекты, хранящиеся в пуле")] private GameObject[] prefabs;
    [SerializeField][Header("Количество объектов, хранящихся в пуле")] private int numberObjects = 10;
    private Dictionary<int, Stack<GameObject>> pool; // Словарь - пул объектов

    private void Awake()
    {
        pool = new Dictionary<int, Stack<GameObject>>();
        
        // Предварительное заполнение пула
        foreach (GameObject prefab in prefabs)
        {
            Stack<GameObject> stackObjects = new Stack<GameObject>(); // Стек объектов
            for (int i = 0; i < numberObjects; i++)
            {
                GameObject gameObject = Instantiate(prefab, transform); // Создаем объект
                gameObject.SetActive(false); // Скрываем объект
                stackObjects.Push(gameObject); // Добавляем в стек
            }

            pool.Add(prefab.GetInstanceID(), stackObjects); // Добавляем в пул
        }
    }

    /// <summary>
    /// Получает объект из пула объектов.
    /// </summary>
    /// <param name="prefab">Префаб</param>
    /// <returns>Объект на сцене</returns>
    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        int objectId = prefab.GetInstanceID(); // Получаем id префаба

        if (pool.TryGetValue(objectId, out Stack<GameObject> stackObjects)) // Получаем объект из словаря
        {
            if (stackObjects.Count > 0) // Если объекты есть
            {
                GameObject gameObject = stackObjects.Pop(); // Возвращаем и удаляем верхний элемент стека
                gameObject.SetActive(true); // Активируем объект
                gameObject.transform.position = position; // Устанавливаем позицию
                gameObject.transform.rotation = rotation; // Устанавливаем поворот
                gameObject.transform.parent = parent; // Группируем
                return gameObject;
            }
            else
            {
                return Instantiate(prefab); // Создаем новый
            }
        }
        else
        {
            return null; // Объекта не было в пуле
        }
    }

    /// <summary>
    /// Возвращает объект в пул объектов.
    /// </summary>
    /// <param name="gameObject">Объект на сцене</param>
    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false); // Скрываем объект
        gameObject.transform.parent = transform; // Группируем
        int objectId = gameObject.GetInstanceID(); // Получаем id объекта

        if (pool.TryGetValue(objectId, out Stack<GameObject> stackObjects)) // Получаем объект из словаря
        {
            stackObjects.Push(gameObject); // Добавляем в стек
        }
        else
        {
            stackObjects = new Stack<GameObject>(); // Стек объектов
            stackObjects.Push(gameObject); // Добавляем в стек
            pool.Add(objectId, stackObjects); // Добавляем в пул
        }
    }
}
