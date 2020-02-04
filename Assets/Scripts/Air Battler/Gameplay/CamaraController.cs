using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [SerializeField, Range(0, 10f)]
    float followSpeed = 5f;
    [SerializeField, Range(0f,10f)]
    float startZoomSpeed;
    [SerializeField]
    int gameCameraZoomHeight;

    [HideInInspector]
    public Vector2 screenSize;

    bool startFollowing;
    Transform player;
    Vector3 offsetFromPlayer;
    Camera cam;
    bool playerDead = false;

    void Start()
    {
        cam = Camera.main;
        //StartFollow();
        screenSize = new Vector2(Screen.width, Screen.height);
        GamePlayController.instance.ConnectToOnPlayerDeath(OnPlayerDeath);
        GamePlayController.instance.onGameStart += StartFollow;
    }

    private void FixedUpdate()
    {
        if (startFollowing && !playerDead)
        {
            Vector3 newPos = offsetFromPlayer + player.position;
            transform.localPosition = Vector3.Lerp(transform.position, newPos, Time.deltaTime * followSpeed);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, gameCameraZoomHeight, Time.deltaTime * startZoomSpeed);
        }else if (playerDead)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, gameCameraZoomHeight - 15, Time.deltaTime * startZoomSpeed);
        }
    }
    public void StartFollow()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if(player != null)
        {
            offsetFromPlayer = transform.position - player.position;
            startFollowing = true;
        }
            
    }

    void OnPlayerDeath()
    {
        playerDead = true;
    }
}
