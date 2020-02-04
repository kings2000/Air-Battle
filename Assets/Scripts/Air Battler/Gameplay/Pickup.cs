using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    public PickupCategory pickupCategory;
    public ParticleSystem effect;
    public GameObject body;

    [HideInInspector] public bool picked = false;

    public void OnPickup(IDamagable damagable)
    {
        if (!picked)
        {
            damagable.TakeHealth(5);
            effect.Play();
            body.SetActive(false);
            GamePlayController.instance.pickupFactory.Recliam(this, effect.main.startLifetime.constant);
        }
        picked = true;
    }
}
