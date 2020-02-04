using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Air Battle Factories/" + (nameof(BulletFactory)))]
public class BulletFactory : ObjectFactories
{

    public Bullet bulletPref;

    public Bullet Get()
    {
        Bullet instance = CreateObject(bulletPref);
        instance.Setup(this);
        return instance;
    }
    public void Reclaim(Bullet obj, float time = 0.0f)
    {
        Destroy(obj.gameObject, time);
    }
}
