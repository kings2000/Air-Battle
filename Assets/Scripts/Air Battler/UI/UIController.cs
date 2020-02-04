using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Button shootButton;

    Planes playerPlane;

    void Start()
    {
        //shootButton.onClick.AddListener(delegate { OnShoot(); });
        playerPlane = GameObject.FindGameObjectWithTag("Player").GetComponent<Planes>();
        
    }

    void OnShoot()
    {
        //playerPlane.Shoot();
    }
}
