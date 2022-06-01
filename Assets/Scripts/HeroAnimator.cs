using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField][Header("Animator")] private Animator animator;
    private static readonly int Angry = Animator.StringToHash("Angry");

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
        {
            animator.SetTrigger(Angry); // Проигрывание анимации
        }
    }
}
