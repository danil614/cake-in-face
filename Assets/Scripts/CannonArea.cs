using UnityEngine;
using UnityEngine.EventSystems;

public class CannonArea : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler
{
    private bool isPressing = false;

    [HideInInspector]
    public bool IsPressing { get { return isPressing; } }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Hero"))
    //    {
    //        Debug.Log("CannonArea OnCollisionEnter2D");
    //        isPressing = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("CannonArea OnCollisionExit2D");
    //    isPressing = false;
    //}

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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Hero"))
    //    {
    //        Debug.Log("CannonArea OnTriggerStay2D");
    //        isPressing = true;
    //    }
    //}
}
