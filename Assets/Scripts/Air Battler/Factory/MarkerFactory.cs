using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum MarkerCategory { Health, Missile, Planes}

[CreateAssetMenu(menuName = "Air Battle Factories/" + (nameof(MarkerFactory)))]
public class MarkerFactory : ObjectFactories
{
   public List<Marker> markers;
    
   public Marker Get(MarkerCategory markerCategory)
   {
        Marker marker = CreateObject(markers.Where(x => x.markerCategory == markerCategory).FirstOrDefault(), false);
        return marker;
   }

    public void Reclaim(Marker marker)
    {
        Destroy(marker.gameObject);
    }
}
