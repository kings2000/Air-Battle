using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSkin : MonoBehaviour
{
    public PlaneSkineCategory planeSkineCategory;
    public GameObject planeBlade;

    [Range(0f, 100f)]
    public float speed;
    [Range(0f, 120f)]
    public float rotateSpeed = 10;
    [Range(0f, 100f)]
    public float acceleration = 10f;
}
