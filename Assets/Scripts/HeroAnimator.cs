using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    private static readonly int Angry = Animator.StringToHash("Angry");
    [SerializeField] [Header("Animator")] private Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake")) animator.SetTrigger(Angry); // Проигрывание анимации
    }
}