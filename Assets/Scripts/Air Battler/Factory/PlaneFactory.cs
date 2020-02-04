using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum PlaneSkineCategory { skin1, skin2, skin3, skin4}

[CreateAssetMenu(menuName = "Air Battle Factories/" + (nameof(PlaneFactory)))]
public class PlaneFactory : ObjectFactories
{

    public Planes playerPref;
    public Planes botsPref;

    public List<PlaneSkin> planeSkins;

    public PlaneSkin Get(PlaneSkineCategory skin)
    {
        PlaneSkin instance = CreateObject(planeSkins.Where(x => x.planeSkineCategory == skin).FirstOrDefault(), false);
        return instance;
    }

    public Planes Get(PlaneCategory planeCategory)
    {
        Planes current = (planeCategory == PlaneCategory.Player) ? playerPref : botsPref;
        Planes ins = CreateObject(current, false);
        return ins;
    }

    public PlaneSkineCategory GetRandomSkin()
    {
        int rand = Random.Range(0, 4);
        return (PlaneSkineCategory)rand;
    }

    public void ReclaimSkin(PlaneSkin o, float time = 0.0f, bool hide = false)
    {
        if (hide)
        {
            o.gameObject.SetActive(false);
            
        }
        else
        {
            Destroy(o.gameObject, time);
        }
        
    }

    public void Reclaim(Planes o, float time = 0.0f)
    {
        Destroy(o.gameObject, time);
    }
}
