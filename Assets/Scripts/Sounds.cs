using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    /// <summary>
    ///     Запустить звук.
    /// </summary>
    public void StartSound()
    {
        if (!audioSource.isPlaying) audioSource.Play();
    }
}