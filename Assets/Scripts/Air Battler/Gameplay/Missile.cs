using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public Transform body;
    [SerializeField, Range(0, 100)]
    float speed = 70f;
    [SerializeField, Range(0, 100)]
    float acceleration = 50f;
    [SerializeField, Range(0, 200)]
    float rotateSpeed = 20f;

    float bodyRotateSpeed = 5;
    Transform target;
    Vector3 velocity;
    Rigidbody body3d;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        body3d = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        FollowTarget();
    }

    void RotateBody()
    {
        body.Rotate(0, 0, Time.deltaTime * 90 * bodyRotateSpeed);
    }

    void FollowTarget()
    {
        /*
        Vector3 playerPosDiff = (target.position - transform.position);
        Vector3 normalDir = playerPosDiff.normalized;
        Vector2 direction = new Vector2(normalDir.x, normalDir.z);
        Vector3 bodyangle = new Vector3(0, 0, -(direction.y + direction.x) * 20f);
        if (direction != Vector2.zero)
        {
            if (playerPosDiff.magnitude <= 8f)
            {
                float randV = Random.Range(-5, 5);
                direction.x = Mathf.Sign(randV) * direction.x;
                direction.y = Mathf.Sign(randV) * direction.y;
            }
            float angle = (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            Quaternion newRot = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, rotateSpeed);
        }
        */

        //Quaternion rot = Quaternion.LookRotation(target.position - transform.position);
       // body3d.MoveRotation(Quaternion.RotateTowards(transform.rotation, rot, rotateSpeed * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        Vector3 desiredVelocity = transform.forward * speed;
        float changeSpeed = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, changeSpeed);
        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, changeSpeed);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, changeSpeed);

        Vector3 motion = desiredVelocity * Time.deltaTime;
        //transform.localPosition += motion;
        body3d.MovePosition(body3d.position + motion);
        Vector3 dir = target.position - body3d.position;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir, transform.forward).y;
        body3d.angularVelocity = new Vector3(0, -rotateAmount * rotateSpeed * Time.fixedDeltaTime, 0); 
        ////float angle = 90 - (Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg);
        //Quaternion rotTo = Quaternion.AngleAxis(angle, Vector3.up);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotTo, Time.deltaTime * rotateSpeed);

        //body3d.velocity = motion;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
