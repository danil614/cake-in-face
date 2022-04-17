using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    private ParticleSystem particle; // Система частиц
    private ObjectPool objectPool; // Пул объектов

    [HideInInspector] public ObjectPool ObjectPool { set { objectPool = value; } }

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!particle.isPlaying)
        {
            objectPool.ReturnObject(gameObject); // Убираем в пул объектов
        }
    }

    /// <summary>
    /// Запускает систему частиц.
    /// </summary>
    public void StartParticleSystem()
    {
        particle.Play();
    }
}
