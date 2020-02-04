using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType { AK47}
public class TopGun : MonoBehaviour
{
    public GunType gunType;

    [SerializeField]
    Transform shootPoint;
    [SerializeField, Range(0f,1f)]
    float shootTreshold = .2f;

    public Vector3 startRotation;
    [SerializeField]
    ParticleSystem shootEffect;

    float shootTime;

    private void Start()
    {
        transform.localRotation = Quaternion.Euler(startRotation);
    }

    public void Shoot()
    {
        if(shootTime < Time.time)
        {
            Bullet bullet = GamePlayController.instance.GetBullet();
            
            if (bullet == null)
            {
                bullet = GamePlayController.instance.bulletFactory.Get();
                GamePlayController.instance.AddBullet(bullet);
            }
            //bullet.shot = true;
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = transform.parent.rotation;
            shootTime = Time.time + shootTreshold;
            bullet.gameObject.SetActive(true);
            bullet.shot = true;
            bullet.currentShooter = GetComponentInParent<Planes>().ID;
            //play Audio
            AudioManager.instance.PlayShot(AudioManager.instance.audioFactory.ak47);
            shootEffect.Play();
        }
    }
}
