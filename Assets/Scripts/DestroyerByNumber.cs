using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerByNumber : MonoBehaviour
{
    [SerializeField] [Header("Допустимое количество объектов на сцене")]
    private int allowedNumber;

    [SerializeField] [Header("Пул объектов")]
    private ObjectPool objectPool;

    [SerializeField] [Header("Задержка удаления объектов")]
    private float delay;

    private LinkedList<GameObject> _objectsOnScene; // Коллекция для хранения объектов на сцене

    private void Awake()
    {
        _objectsOnScene = new LinkedList<GameObject>(); // Инициализируем коллекцию
    }

    private void Start()
    {
        StartCoroutine(DeleteOldObjectByNumber()); // Удаление тортов по количеству
    }

    /// <summary>
    ///     Удаляет торты по количеству с задержкой.
    /// </summary>
    private IEnumerator DeleteOldObjectByNumber()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            DeleteOldObject(); // Удаляем самый старый торт
        }
    }

    /// <summary>
    ///     Добавляет объект в коллекцию.
    /// </summary>
    public void AddToCollection(GameObject gameObject)
    {
        if (_objectsOnScene != null && !_objectsOnScene.Contains(gameObject))
            _objectsOnScene.AddLast(gameObject); // Добавляем в конец списка
    }

    /// <summary>
    ///     Удаляет объект из коллекции.
    /// </summary>
    public void DeleteFromCollection(GameObject gameObject)
    {
        if (_objectsOnScene != null && _objectsOnScene.Count > 0)
            _objectsOnScene.Remove(gameObject); // Удаляем объект из списка
    }

    /// <summary>
    ///     Удаляет объекты со сцены при превышении допустимого количества.
    /// </summary>
    private void DeleteOldObject()
    {
        if (_objectsOnScene != null && _objectsOnScene.Count > allowedNumber)
        {
            var oldObject = _objectsOnScene.First.Value; // Получаем самый старый объект
            GameManager.StartSmoothDestroyer(oldObject, false); // Плавно удаляем со сцены
        }
    }
}