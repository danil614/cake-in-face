using UnityEngine;

public class CakeSpot : MonoBehaviour
{
    Animator animator;

	private void Start()
	{
        try
        {
            animator = GetComponent<Animator>();
        }
        catch (MissingComponentException)
        {
            animator = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
        {
            if (animator != null)
            {
                animator.SetTrigger("Angry"); // Проигрывание анимации
            }
        }
    }
}
