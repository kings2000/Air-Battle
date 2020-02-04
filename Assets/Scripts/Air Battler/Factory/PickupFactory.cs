using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum PickupCategory { health, initSpeed }

[CreateAssetMenu(menuName = "Air Battle Factories/"+(nameof(PickupFactory)))]
public class PickupFactory : ObjectFactories
{
    public List<Pickup> pickups;

    public Pickup Get(PickupCategory pickupCategory)
    {
        Pickup ins = CreateObject(pickups.Where(x => x.pickupCategory == pickupCategory).FirstOrDefault());
        return ins;

    }

    public void Recliam(Pickup pickup, float time = 0.0f)
    {
        Destroy(pickup.gameObject, time);
    }
    
}
