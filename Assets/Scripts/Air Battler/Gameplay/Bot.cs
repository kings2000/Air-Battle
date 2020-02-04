using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Planes
{

    Transform playerPlane;
    bool clickedToPlayer;
    bool playerDead = false;

    protected override void Start()
    {
        base.Start();
        playerPlane = GameObject.FindGameObjectWithTag("Player").transform;
        GamePlayController.instance.ConnectToOnPlayerDeath(OnPlayerDeath);
        speed += 5;
    }

    void OnPlayerDeath()
    {
        playerDead = true;
    }
    
    protected override void Update()
    {
        base.Update();
        if (dead || playerDead)
        {
            return;
        }
        //Vector3 forwardTarget = transform.position + (transform.forward * 50);
        Vector3 playerPosDiff = (playerPlane.position - transform.position);
        Vector3 normalDir = playerPosDiff.normalized;
        Vector2 direction = new Vector2(normalDir.x, normalDir.z);
        //Debug.Log(playerPosDiff);
        Vector3 bodyangle = new Vector3(0, 0, -(direction.y + direction.x) * 20f);
        if (direction != Vector2.zero)
        {
            if(playerPosDiff.magnitude <= 8f)
            {
                float randV = Random.Range(-5, 5);
                direction.x = Mathf.Sign(randV) * direction.x;
                direction.y = Mathf.Sign(randV) * direction.y;
            }
            //float angle = (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            //Quaternion newRot = Quaternion.Euler(0, angle, 0);
           
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
            //planeBody.localRotation = Quaternion.Lerp(planeBody.localRotation, Quaternion.Euler(bodyangle), Time.deltaTime * 10);
        }
        else
        {
            planeBody.localRotation = Quaternion.Lerp(planeBody.localRotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * 5);
        }
        LocateClosePlayer();
    }


    protected override void FixedUpdate()
    {
        //acceleration = 70;
        base.FixedUpdate();
        Vector3 dir = (playerPlane.position - transform.position);
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir, transform.forward).y;
        body.angularVelocity = new Vector3(0, -rotateAmount * 140f * Time.deltaTime, 0);
    }


    float ScanRaduis = 35;
    float maxAngleToShoot = 10;
    bool enemyLocked = false;
    GameObject currentTarget;


    void LocateClosePlayer()
    {
        if (dead | playerDead) return;
        if (!enemyLocked)
        {

            float distance = float.MaxValue;
            
            float dis = Vector3.Distance(playerPlane.position, transform.position);
            bool dead = playerPlane.GetComponent<Planes>().dead;

            if (dis < ScanRaduis && !dead)
            {
                distance = dis;
                enemyLocked = true;
            }
           
        }
        else
        {
            Vector3 dir = playerPlane.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, ScanRaduis + 20f, targetMask, QueryTriggerInteraction.Collide))
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
            float dis = Vector3.Distance(playerPlane.transform.position, transform.position);
            if (dis > ScanRaduis)
            {
                enemyLocked = false;

            }
        }
    }
}
