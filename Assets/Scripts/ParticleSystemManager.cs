using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    private ObjectPool objectPool; // Пул объектов

    private void Awake()
    {
        objectPool = GameObject.Find(ObjectPool.ObjectPoolName).GetComponent<ObjectPool>();
    }

    private void OnParticleSystemStopped()
    {
        objectPool.ReturnObject(gameObject); // Убираем в пул объектов
    }
}
