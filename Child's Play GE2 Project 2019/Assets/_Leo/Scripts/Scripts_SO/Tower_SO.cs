using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Tower", menuName = "Actos/Towers")]
public class Tower_SO : ScriptableObject
{
    public string description;
    public float range;
    public float rateOfFire;
    public float innerRadius;
    public GameObject projectilePrefab;
}
