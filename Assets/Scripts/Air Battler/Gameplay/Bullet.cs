using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField, Range(0f, 200f)]
    float speed = 40f;
    public BulletFactory bulletFactory { private get; set; }

    float lifeTime = 5f;
    [HideInInspector]public bool shot = false;
    [HideInInspector] public string currentShooter;
    [HideInInspector] public Vector3 initialVelocity;
    

    public void Setup(BulletFactory factory)
    {
        bulletFactory = factory;
        //bulletFactory.Reclaim(this, 5f);
    }

    
    void Update()
    {
        
        if (shot)
        {
            transform.position += initialVelocity + (transform.forward * speed * Time.deltaTime);
            if (lifeTime <= 0)
            {
                ResetBullet();
            }
            lifeTime -= Time.deltaTime;
        }
        
    }

    public void ResetBullet()
    {
        shot = false;
        transform.position = Vector3.zero;
        lifeTime = 5f;
        currentShooter = "None";
        gameObject.SetActive(false);
        initialVelocity = Vector3.zero;
    }
}
