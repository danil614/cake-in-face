using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    private static readonly int Angry = Animator.StringToHash("Angry");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cake"))
        {
            _animator.SetTrigger(Angry); // Проигрывание анимации
            transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
        }
    }
}