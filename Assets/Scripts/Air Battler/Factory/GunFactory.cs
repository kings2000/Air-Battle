using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Air Battle Factories/" + (nameof(GunFactory)))]
public class GunFactory : ObjectFactories
{
    public List<TopGun> guns;

    public TopGun Get(GunType gunType)
    {
        TopGun gun = CreateObject(guns.Where(x => x.gunType == gunType).FirstOrDefault(), false);
        return gun;
    }

    public void Reclaim(TopGun o, float time = 0.0f)
    {
        Destroy(o.gameObject, time);
    }
}
