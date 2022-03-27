using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = eventData.pointerCurrentRaycast.screenPosition;
        //eventData.pointerCurrentRaycast.gameObject.transform.position = Input.mousePosition;
        //Debug.Log("OnDrag!");
        //eventData.pointerCurrentRaycast.gameObject.transform.position = eventData.pointerCurrentRaycast.screenPosition;
        //GetComponent<Rigidbody2D>().MovePosition(eventData.position);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }
}
