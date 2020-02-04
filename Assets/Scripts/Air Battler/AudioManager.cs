using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioFactory audioFactory;

    List<AudioSource> gunShotSource;

    private void Awake()
    {
        instance = this;
        gunShotSource = new List<AudioSource>();
    }
    public void PlayShot(AudioClip clip)
    {
        if(gunShotSource.Count <= 0)
        {
            //Create a new audio
            AudioSource obj = audioFactory.Get();
            
            gunShotSource.Add(obj);
        }
        bool played = false;
        for (int i = 0; i < gunShotSource.Count; i++)
        {
            if (!gunShotSource[i].isPlaying)
            {
                gunShotSource[i].PlayOneShot(clip);
                played = true;
                break;
            }
        }
        if (!played)
        {
            AudioSource obj = audioFactory.Get();
            obj.PlayOneShot(clip);
            
            gunShotSource.Add(obj);
        }
    }

    
}
