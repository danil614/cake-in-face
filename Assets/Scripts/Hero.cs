using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    Vector3 startClick;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startClick = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //startClick = //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //startPoint = transform.position;//Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
        Debug.Log("Start Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = eventData.pointerCurrentRaycast.screenPosition;
        //eventData.pointerCurrentRaycast.gameObject.transform.position = Input.mousePosition;
        //Debug.Log("OnDrag!");
        //eventData.pointerCurrentRaycast.gameObject.transform.position = eventData.pointerCurrentRaycast.screenPosition;
        //GetComponent<Rigidbody2D>().MovePosition(eventData.position);

        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = mousePosition;
        Vector3 endClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 clickVector = endClick - startClick;
        //clickVector -= new Vector2(transform.position.x, transform.position.y);
        //transform.position += clickVector;

        GetComponent<Rigidbody2D>().MovePosition(clickVector);
    }
}
