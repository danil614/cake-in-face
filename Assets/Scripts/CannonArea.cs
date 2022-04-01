using UnityEngine;
using UnityEngine.EventSystems;

public class CannonArea : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float speed;

    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position; // ”станавливаем начальную позицию пушки
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, startPosition, speed * Time.deltaTime);
    }

    //private bool isPressing = false;

    //[HideInInspector]
    //public bool IsPressing { get { return isPressing; } }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    //Debug.Log("CannonArea OnPointerEnter");
    //    //isPressing = true;
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Debug.Log("CannonArea OnPointerExit");
    //    isPressing = false;
    //}

    ////private void OnCollisionEnter2D(Collision2D collision)
    ////{
    ////}

    ////private void OnTriggerEnter2D(Collider2D collision)
    ////{

    ////}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Hero"))
    //    {
    //        Debug.Log("CannonArea OnTriggerStay2D");
    //        isPressing = true;
    //    }
    //}
}
