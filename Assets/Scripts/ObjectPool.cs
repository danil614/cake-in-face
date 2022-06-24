using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public const string ObjectPoolName = "ObjectPool";

    [SerializeField] [Header("Объекты, хранящиеся в пуле")]
    private GameObject[] prefabs;

    [SerializeField] [Header("Начальное количество объектов, хранящихся в пуле")]
    private int numberObjects;

    [SerializeField] [Header("Разрушитель тортов по количеству")]
    private DestroyerByNumber cakeDestroyerByNumber;

    private Dictionary<string, Stack<GameObject>> _pool; // Словарь - пул объектов

    private void Awake()
    {
        _pool = new Dictionary<string, Stack<GameObject>>();

        // Предварительное заполнение пула
        foreach (var prefab in prefabs)
        {
            // Стек объектов
            var stackObjects = new Stack<GameObject>();
            for (var i = 0; i < numberObjects; i++)
            {
                // Создаем объект
                var item = Instantiate(prefab, transform);
                item.SetActive(false); // Скрываем объект
                item.name = prefab.name; // Меняем имя
                stackObjects.Push(item); // Добавляем в стек
            }

            _pool.Add(prefab.name, stackObjects); // Добавляем в пул
        }
    }

    /// <summary>
    ///     Получает объект из пула объектов.
    /// </summary>
    /// <returns>Объект на сцене</returns>
    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        // Получаем объект из словаря
        if (_pool.TryGetValue(prefab.name, out var stackObjects))
        {
            GameObject item;
            if (stackObjects.Count > 0) // Если объекты есть
            {
                // Возвращаем и удаляем верхний элемент стека
                item = stackObjects.Pop();
                item.SetActive(true); // Активируем объект
            }
            else
            {
                item = Instantiate(prefab); // Создаем новый
                item.name = prefab.name; // Меняем имя
            }

            SetTransform(item.transform, position, rotation, parent);
            return item;
        }

        return null; // Объекта не было в пуле
    }

    /// <summary>
    ///     Возвращает объект в пул объектов.
    /// </summary>
    /// <param name="item">Объект на сцене</param>
    public void ReturnObject(GameObject item)
    {
        item.SetActive(false); // Скрываем объект
        item.transform.parent = transform; // Группируем
        // Получаем объект из словаря
        if (_pool.TryGetValue(item.name, out var stackObjects))
        {
            // Если в стеке нет такого объекта, то добавляем
            if (!stackObjects.Contains(item)) stackObjects.Push(item);
        }
        else
        {
            stackObjects = new Stack<GameObject>(); // Стек объектов
            stackObjects.Push(item); // Добавляем в стек
            _pool.Add(item.name, stackObjects); // Добавляем в пул
        }

        // Удаляем торт из коллекции тортов
        if (item.CompareTag("Cake")) cakeDestroyerByNumber.DeleteFromCollection(item);
    }

    /// <summary>
    ///     Устанавливает позицию, поворот и группирует.
    /// </summary>
    private static void SetTransform(Transform transform, Vector3 position, Quaternion rotation, Transform parent)
    {
        transform.parent = null; // Снимаем группировку
        transform.SetPositionAndRotation(position, rotation); // Устанавливаем позицию и поворот
        transform.parent = parent; // Группируем
    }
}