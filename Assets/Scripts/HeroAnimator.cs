using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
        {
            animator.SetTrigger("Angry"); // Проигрывание анимации
        }
    }
}
