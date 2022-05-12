using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    [SerializeField][Header("Animator")] private Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
        {
            animator.SetTrigger("Angry"); // Проигрывание анимации
            //transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
        }
    }
}
