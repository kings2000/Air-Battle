using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool isMobile;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            instance = null;
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        isMobile = Application.isMobilePlatform;
    }

    

    // Update is called once per frame
    void Update()
    {
        PlayerInput.Update();
    }
}
