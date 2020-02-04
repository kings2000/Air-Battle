using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{

    public MarkerCategory markerCategory;
    [HideInInspector]public Transform target = default;
    Vector2 screen;
    Vector2 size;
    Vector2 boundMax;
    Vector2 boundMin;
    [SerializeField]Image image;
    void Start()
    {
        screen = new Vector2(Screen.width, Screen.height) / 2;
        size = GetComponent<RectTransform>().rect.size / 2;
        //image = GetComponent<Image>();
        boundMin = new Vector2(-screen.x + size.x, -screen.y + size.y);
        boundMax = new Vector2(screen.x - size.x, screen.y - size.y);
    }

    public void Process()
    {
        if(target != null)
        {
            Vector3 toScreen = Camera.main.WorldToScreenPoint(target.position) - new Vector3(screen.x, screen.y);

            float x = Mathf.Clamp(toScreen.x,boundMin.x, boundMax.x);
            float y = Mathf.Clamp(toScreen.y, boundMin.y, boundMax.y);

            bool hide = (x > boundMin.x && x < boundMax.x ) && (y > boundMin.y && y < boundMax.y);
            Hide(!hide);
          

            Vector3 newPos = new Vector3(x, y, 0);
            transform.localPosition = newPos;
            Vector3 diff = (transform.localPosition - toScreen).normalized;
            float angle = 180 + Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            
        }
    }

    void Hide(bool value)
    {
        image.enabled = value;
       
    }

    
}
