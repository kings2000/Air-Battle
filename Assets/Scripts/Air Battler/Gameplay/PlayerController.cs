using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Planes
{

    public bool inversY;
    public bool InversX;
    public bool fixedTurn;


    bool startMove = false;
    Vector3 startClickPosition;
    Vector2 wheelDirection;

    protected override void Start()
    {
        isPlayer = true;
        base.Start();
        center = new Vector2(Screen.width, Screen.height) / 2;
        PlayerInput.Connect(this);
        GamePlayController.instance.onGameStart += StartMove;
    }

    public void StartMove()
    {
        startMove = true;
        GamePlayController.instance.ConnectPlayer(this);
        GamePlayController.instance.onGameStart -= StartMove;
    }

    protected override void FixedUpdate()
    {
        if (!startMove) return;
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
        if (dead || !startMove)
        {
            return;
        }
        wheelDirection = WheelController.Direction;
       // wheelDirection.y = -wheelDirection.y;

        if (InversX)
            wheelDirection.x = -wheelDirection.x;
        //if (inversY)
           // wheelDirection.y = -wheelDirection.y;

        Vector3 bodyangle = new Vector3(0, 0, -(wheelDirection.x) * 20f);
        if (wheelDirection != Vector2.zero)
        {
            float angle = wheelDirection.x * (Mathf.Rad2Deg);
            Quaternion newRot = Quaternion.Euler(0, angle, 0);
            if (fixedTurn)
            {
                newRot*= transform.rotation;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation,  newRot, rotateSpeed * Time.deltaTime);
            planeBody.localRotation = Quaternion.Lerp(planeBody.localRotation, Quaternion.Euler(bodyangle), Time.deltaTime * 10);
        }
        else
        {
            
            planeBody.localRotation = Quaternion.Lerp(planeBody.localRotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * 5);
        }
        /*
        if (canShoot)
        {
            if(shootTime < Time.time)
            {
                for (int i = 0; i < shootPoints.Length; i++)
                {
                    Bullet bullet = GamePlayController.instance.GetBullet();
                    if(bullet == null)
                    {
                        bullet = GamePlayController.instance.bulletFactory.Get();
                        GamePlayController.instance.AddBullet(bullet);
                    }
                    
                    bullet.transform.position = shootPoints[i].position;
                    bullet.transform.rotation = transform.localRotation;
                    bullet.gameObject.SetActive(true);
                    bullet.shot = true;
                }

                shootTime = Time.time + shootTreshold;
            }
        }
        */
        LocateClosePlayer();
    }

    float ScanRaduis = 45;
    float maxAngleToShoot = 10;
    bool enemyLocked = false;
    GameObject currentTarget;


    void LocateClosePlayer()
    {
        if (dead) return;
        if (!enemyLocked)
        {
            GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");
            if (bots.Length > 0 )
            {
                float distance = float.MaxValue;
                GameObject closeEnemy = null;
                for (int i = 0; i < bots.Length; i++)
                {   
                    if(bots[i] != null)
                    {
                        float dis = Vector3.Distance(bots[i].transform.position, transform.position);
                        bool dead = bots[i].GetComponent<Bot>().dead;
                        if (dis <= ScanRaduis && dis < distance && !dead)
                        {
                            distance = dis;
                            closeEnemy = bots[i];
                        }
                    }
                    
                };
                if(closeEnemy != null)
                {
                    
                    currentTarget = closeEnemy;
                    enemyLocked = true;
                }
            }
        }
        else
        {
            bool dead = currentTarget.GetComponent<Bot>().dead;
            if (!dead)
            {
                Vector3 dir = currentTarget.transform.position - transform.position;
                Ray ray = new Ray(transform.position, dir);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, ScanRaduis + 20f, targetMask, QueryTriggerInteraction.Collide))
                {
                    if (hit.collider != null)
                    {
                        //Transform the gun to face the target
                        gunHolder.LookAt(hit.point, transform.up);

                        //Make a check to know if the gun is facing the target
                        float faceAngle = Vector3.Angle(gunHolder.transform.forward, hit.point - gunHolder.position);
                        if (faceAngle < maxAngleToShoot)
                        {
                            currentTopGun.Shoot();
                        }

                        
                    }
                    
                }
                float dis = Vector3.Distance(currentTarget.transform.position, transform.position);
                if (dis > ScanRaduis)
                {
                    enemyLocked = false;
                    currentTarget = null;
                }
            }
            else
            {
                enemyLocked = false;
                currentTarget = null;
            }
            
        }
        
        
    }

    Vector2 center;

    #region Input Signal Listening
    public override void OnInputDown(Vector3 pointerPosition) {
        startClickPosition = pointerPosition;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
        if(Physics.Raycast(ray, out hit, 1000, targetMask))
        {
            //Debug.Log(hit.collider.name);
            if(hit.collider.tag == "Bot")
            {
                Debug.Log(hit.collider.gameObject.GetComponentInParent<Bot>().name);
            }
        }
    }

    public override void OnInputUp(Vector3 pointerPosition) {

    }

    public override void OnInput(Vector3 pointerPosition) {

    }
    #endregion
}
