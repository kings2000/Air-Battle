using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Air Battle Factories/" + (nameof(AudioFactory)))]
public class AudioFactory : ObjectFactories
{
    public AudioClip ak47;
    public AudioClip planeEngin;
    public AudioClip planeDie;

    public AudioSource Get()
    {
        GameObject newSource = new GameObject("Sound");
        MoveObjetToScene(newSource);
        AudioSource s = newSource.AddComponent<AudioSource>();
        return s;
    }

    public void Reclaim(AudioSource s, float killTime = 0.0f)
    {
        Destroy(s, killTime);
    }
}
