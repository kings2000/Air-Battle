using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : EventTrigger
{

    bool shootDown;

    Planes playerPlane;

    void Start()
    {
        playerPlane = GameObject.FindGameObjectWithTag("Player").GetComponent<Planes>();
    }

    public override void OnPointerClick(PointerEventData data)
    {
        //Debug.Log("Event");
    }
    public override void OnPointerDown(PointerEventData data)
    {
        playerPlane.Shoot(true);
    }

    public override void OnPointerEnter(PointerEventData data)
    {

    }
    public override void OnPointerUp(PointerEventData data)
    {
        playerPlane.Shoot(false);
    }

    
}
