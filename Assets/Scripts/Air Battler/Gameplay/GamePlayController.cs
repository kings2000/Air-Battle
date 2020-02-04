using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

//public delegate void OnPlayerDeath();

public class GamePlayController : MonoBehaviour
{

    public static GamePlayController instance;

    #region Properties

    [Header("Factories")]
    public AudioFactory audioFactory;
    public BulletFactory bulletFactory;
    public PlaneFactory planeFactory;
    public GunFactory gunFactory;
    public MarkerFactory markerFactory;
    public PickupFactory pickupFactory;


    [Header("Spawning Properties")]
    [SerializeField] int maxEnemyCount = 5;
    [HideInInspector]public int currentEnemyCount;
    float maxSpawnRaduis = 200;
    float spawnTimeTreshold = 2;
    float spawnTimeTracker;
    float playerYOffset;

    public static List<Bullet> bullets;
    Transform playerTransform;
    bool startSpawning;
    bool playerDead = false;
    public Action onPlayerDeath;
    public Action onGameStart;
    #endregion

    #region Unity Functions

    int totalEnemySpawned = 0;
    public bool gameStarted = false;

    private void Awake()
    {
        instance = this;
        bullets = new List<Bullet>();
        spawnTimeTracker = spawnTimeTreshold;
    }

    public void StartGamePlay()
    {
        onGameStart?.Invoke();
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().StartMove();
        gameStarted = true;

        //Time.timeScale = .5f;
    }

    private void Update()
    {
        
        if(currentEnemyCount < maxEnemyCount & startSpawning)
        {
            if(spawnTimeTracker < Time.time)
            {
                spawnTimeTracker = Time.time + spawnTimeTreshold;
                //create enemy (No factory) at position
                Vector2 randDir = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)) * maxSpawnRaduis;
                Vector3 pos = playerTransform.position + new Vector3(randDir.x,0, randDir.y);
                CreatePlane(PlaneCategory.Bots, pos);
                currentEnemyCount++;
            }
        }
    }

    #endregion


    #region Spawning System

    Planes CreatePlane(PlaneCategory planeCategory, Vector3 position)
    {
        Planes plane = planeFactory.Get(planeCategory);
        plane.GetSkin(planeFactory.GetRandomSkin());
        plane.ID = "Bot" + totalEnemySpawned;
        plane.transform.position = position;
        totalEnemySpawned++;
        return plane;
    }


    #endregion

    #region Bullet Controll
    public void AddBullet(Bullet bullet)
    {
        bullets.Add(bullet);
    }
    
    public Bullet GetBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {

            if (!bullets[i].gameObject.activeSelf)
            {
                
                return bullets[i];
            }
        }
        return null;
    }
    #endregion

    public void ConnectPlayer(Planes player)
    {
        playerTransform = player.transform;
        playerYOffset = playerTransform.position.y;
        startSpawning = true;
    }

    public void ConnectToOnPlayerDeath(Action _onPlayerDeath)
    {
        onPlayerDeath += _onPlayerDeath;
    }
}
