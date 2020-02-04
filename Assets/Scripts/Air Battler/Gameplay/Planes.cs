using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlaneCategory{Player, Bots, None}

public class Planes : MonoBehaviour , InputSignalListener , IDamagable
{
    public PlaneCategory planeCategory;
    public LayerMask targetMask;
    [SerializeField, Range(0f, 100f)]
    protected float speed;
    [SerializeField, Range(0f, 120f)]
    protected float rotateSpeed = 10;
    [SerializeField, Range(0f, 70f)]
    protected float acceleration = 10f;

    [SerializeField]
    protected Transform planeBody;
   
    [SerializeField]
    protected Transform gunHolder;
    public Transform skinHolder;
    [SerializeField]
    protected AudioSource enginSource;
    public PlaneSkineCategory planeSkinCategory;

    protected TopGun currentTopGun;
    protected Transform planeBlade;
    protected Rigidbody body;
    protected float shootTreshold = .2f;
    protected float shootTime;
    protected bool canShoot;
    protected Vector3 velocity;

    [SerializeField]int startHealth = 10;
    [HideInInspector] public int currentHealth;

    PlaneSkin planeSkin;
    private CollisionPart effectEmitter;
    bool spinBlade;
    public bool dead;
    protected bool isPlayer = false;


    public string ID;

    protected virtual void Start()
    {
        body = GetComponent<Rigidbody>();
        canShoot = false;
        shootTime = Time.time;
        
        currentHealth = startHealth;
        if (dead)
        {
            fallRotation = new Vector3(40, 0, 0) + transform.localEulerAngles;
            enginSource.Stop();
            enginSource.clip = GamePlayController.instance.audioFactory.planeDie;
            enginSource.loop = false;
            enginSource.Play();
        }
        if (isPlayer)
        {
            GetSkin(planeSkinCategory);
            ID = "Player";
        }
        
    }

    protected virtual void Update()
    {
        if(spinBlade)
            planeBlade.Rotate(new Vector3(0, 0, 90 * Time.deltaTime * 40));
    }

    public void GetSkin(PlaneSkineCategory _planeSkin)
    {
        //get skin
        PlaneSkin skin = GamePlayController.instance.planeFactory.Get(_planeSkin);
        skin.transform.SetParent(skinHolder);
        skin.transform.localPosition = Vector3.zero;
        planeBlade = skin.planeBlade.transform;
        planeSkin = skin;

        //attach skin motion prop
        speed = skin.speed;
        acceleration = skin.acceleration;
        rotateSpeed = skin.rotateSpeed;

        //get gun
        currentTopGun = GamePlayController.instance.gunFactory.Get(GunType.AK47);
        currentTopGun.transform.SetParent(gunHolder);
        currentTopGun.transform.localPosition = Vector3.zero;
        spinBlade = true;
        
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        
        Vector3 desiredVelocity = transform.forward * speed;
        float velocityChange = acceleration * Time.deltaTime;
        
        velocity = Vector3.MoveTowards(velocity, desiredVelocity, velocityChange);
        
        body.velocity = velocity;
        if (dead)
        {
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, fallRotation, 1f * Time.deltaTime);
            planeBody.localRotation = Quaternion.Lerp(planeBody.localRotation, planeBody.localRotation * Quaternion.Euler(new Vector3(0, 0, 90)), Time.deltaTime * 3);
        }
    }

    public void Shoot(bool value)
    {
        canShoot = value;
    }

    Vector3 fallRotation;

    public void OnDeath()
    {
        dead = true;
        fallRotation = new Vector3(40, 0, 0) + transform.localEulerAngles;
        enginSource.Stop();
        enginSource.clip = GamePlayController.instance.audioFactory.planeDie;
        enginSource.loop = false;
        enginSource.Play();
        Invoke(nameof(ReclaimSkine), 4.3f);
        GamePlayController.instance.planeFactory.Reclaim(this, 7);
        //on Death oo
        if (isPlayer)
            GamePlayController.instance.onPlayerDeath?.Invoke();
        else
            GamePlayController.instance.currentEnemyCount--;
    }

    void ReclaimSkine()
    {
        GamePlayController.instance.planeFactory.ReclaimSkin(planeSkin,default,true);
    }

    #region IDamagable
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            OnDeath();
            //start fire
            effectEmitter.PlayFire();
        }else if(currentHealth < startHealth/2)
        {
            //start smoke
            effectEmitter.PlaySmoke();
        }
    }
    public void AddEffectEmmiter<T>(T item) where T : MonoBehaviour
    {
        effectEmitter = item as CollisionPart;

    }

    public void TakeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > startHealth / 2)
        {
            //stop smoke
            effectEmitter.StopSmoke();
            
        }
        Debug.Log("Health added " + amount);
    }
    #endregion

    #region Input Signal Listening
    public virtual void OnInputDown(Vector3 pointerPosition){}

    public virtual void OnInputUp(Vector3 pointerPosition){}

    public virtual void OnInput(Vector3 pointerPosition){}
    #endregion
}
