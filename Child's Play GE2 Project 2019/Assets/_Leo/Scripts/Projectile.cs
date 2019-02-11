using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    public float AoERadius = 0.0f;

    public GameObject impactVFX;

    protected Transform _target;
    protected Vector3 direction;
}
