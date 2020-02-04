using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakerController : MonoBehaviour
{

    Dictionary<Transform, Marker> markers;
    List<Transform> currentTarget;
    float timer;

    bool playerDead = false;

    void Start()
    {
        markers = new Dictionary<Transform, Marker>();
        currentTarget = new List<Transform>();
        GamePlayController.instance.ConnectToOnPlayerDeath(OnPlayerDeath);
    }


    public void OnPlayerDeath()
    {
        playerDead = true;

        foreach(Marker m in markers.Values)
        {
            m.gameObject.SetActive(false);
        }
    }

    
    void Update()
    { 
        //scan the world reduce the frame rate
        if(timer < Time.time && !playerDead)
        {
            CreatePlaneMarker();
            CreateHealthMaker();
            for (int i = 0; i < currentTarget.Count; i++)
            {
                if(currentTarget[i] != null && markers.ContainsKey(currentTarget[i]))
                {
                    if(markers[currentTarget[i]].markerCategory == MarkerCategory.Planes)
                    {
                        Bot b = currentTarget[i].GetComponent<Bot>();
                        markers[currentTarget[i]].Process();
                        if (b.dead)
                        {
                            GamePlayController.instance.markerFactory.Reclaim(markers[currentTarget[i]]);
                            markers.Remove(currentTarget[i]);
                            currentTarget.Remove(currentTarget[i]);
                            break;
                        }
                    }else if(markers[currentTarget[i]].markerCategory == MarkerCategory.Health)
                    {
                        Pickup p = currentTarget[i].GetComponent<Pickup>();
                        markers[currentTarget[i]].Process();
                        if (p.picked)
                        {
                            GamePlayController.instance.markerFactory.Reclaim(markers[currentTarget[i]]);
                            markers.Remove(currentTarget[i]);
                            currentTarget.Remove(currentTarget[i]);
                            break;
                        }
                    }
                    
                }
            }
            timer = Time.time + (2f/60f);
        }
    }

    void CreatePlaneMarker()
    {
        GameObject[] planes = GameObject.FindGameObjectsWithTag("Bot");
        for (int i = 0; i < planes.Length; i++)
        {
            Bot b = planes[i].GetComponent<Bot>();
            if (!b.dead)
            {
                if (!markers.ContainsKey(b.transform))
                {
                    Marker marker = GamePlayController.instance.markerFactory.Get(MarkerCategory.Planes);
                    markers.Add(b.transform, marker);
                    currentTarget.Add(b.transform);
                    marker.target = b.transform;
                    marker.transform.SetParent(transform);
                }
            }

        }
    }

    void CreateHealthMaker()
    {
        GameObject[] pickup = GameObject.FindGameObjectsWithTag("Pickup");
        for (int i = 0; i < pickup.Length; i++)
        {
            Pickup b = pickup[i].GetComponent<Pickup>();
            if (!b.picked)
            {
                if (!markers.ContainsKey(b.transform))
                {
                    Marker marker = GamePlayController.instance.markerFactory.Get(MarkerCategory.Health);
                    markers.Add(b.transform, marker);
                    currentTarget.Add(b.transform);
                    marker.target = b.transform;
                    marker.transform.SetParent(transform);
                }
            }

        }
    }
}
