using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WheelController : EventTrigger
{
    
    Transform parentTrans;
    float boundary;
    float threshold = 120;
    Vector2 radius = new Vector2(50, 50);
    bool gameStarted = false;

    public static Vector2 Direction { get; private set; }

    public override void OnPointerClick(PointerEventData data)
    {
        //Debug.Log("Event");
    }
    public override void OnPointerDown(PointerEventData data)
    {
        //Debug.Log("Event");
        
        if (gameStarted == false)
        {
            gameStarted = true;
            GamePlayController.instance.StartGamePlay();
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            float size = GetComponent<RectTransform>().rect.size.x / 2;
            float sign = Mathf.Sign(data.position.x - size);
            Direction = new Vector2(sign, 0);
        }
            
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        
    }
    public override void OnPointerUp(PointerEventData data)
    {
        //transform.localPosition = Vector2.zero;
        Direction = new Vector2(0, 0);
    }
    /*
    private void Update()
    {
        if (transform.localPosition.magnitude > threshold)
        {
            //Direction = transform.localPosition.normalized;
            
        }
        else
        {
            
        }
    }

    public override void OnDrag(PointerEventData data)
    {
        return;
        if (parentTrans == null)
        {
            parentTrans = transform.parent.transform;
            boundary = parentTrans.GetComponent<RectTransform>().rect.width/2;
        }
        Vector2 positionDelta = data.position - new Vector2(parentTrans.position.x, parentTrans.position.y);
        float positionFromCenter = positionDelta.magnitude;
        transform.localPosition = positionDelta;
        Vector2 point = new Vector2(transform.localPosition.x, transform.localPosition.y);

        if(point.magnitude > boundary)
        {
            transform.localPosition = point.normalized * boundary;
        }

        
       
    }
    */
}
