using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPart : MonoBehaviour
{
    [SerializeField]
    ParticleSystem fire, smoke, explosion;
    
    int health = 10;

    string ownerID;
    IDamagable damagable;
    bool exploading = false;
    

    private void Start()
    {
        ownerID = GetComponentInParent<Planes>().ID;
        
        damagable = GetComponentInParent<IDamagable>();
        damagable.AddEffectEmmiter(this);
    }

    public void PlayFire()
    {
        fire.Play();
        Explode();
    }

    public void PlaySmoke()
    {
        smoke.Play();
    }

    public void Explode()
    {
        if (exploading) return;
        
        exploading = true;

        explosion.Play();
    }

    public void StopSmoke()
    {
        smoke.Stop();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag.Equals("Bullet"))
        {
            
            Bullet b = other.GetComponent<Bullet>();
            
            if(b.currentShooter != ownerID)
            {
                damagable.TakeDamage(1);
            }
            
        }else if (other.tag.Equals("Pickup"))
        {
            other.GetComponent<Pickup>().OnPickup(damagable);
        }
    }
    
}
