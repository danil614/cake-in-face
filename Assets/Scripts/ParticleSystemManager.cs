using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    private ObjectPool _objectPool; // Пул объектов

    private void Awake()
    {
        _objectPool = GameObject.Find(ObjectPool.ObjectPoolName).GetComponent<ObjectPool>();
    }

    private void OnParticleSystemStopped()
    {
        _objectPool.ReturnObject(gameObject); // Убираем в пул объектов
    }
}