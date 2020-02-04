using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilsilTageter : MonoBehaviour , InputSignalListener
{
    public LayerMask botMask;
    void Start()
    {
        PlayerInput.Connect(this);
    }

    
    public void OnInputDown(Vector3 pointerPosition)
    {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
        if (Physics.Raycast(ray, out hit, 1000, botMask))
        {
            //Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Bot")
            {
                Transform hh = hit.collider.gameObject.GetComponentInParent<Bot>().transform;
                transform.position = hh.position;
            }
        }
    }

    public void OnInputUp(Vector3 pointerPosition)
    {

    }

    public void OnInput(Vector3 pointerPosition)
    {

    }
}
