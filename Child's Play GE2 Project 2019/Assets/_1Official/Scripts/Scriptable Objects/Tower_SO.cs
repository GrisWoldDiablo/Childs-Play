using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectTileType { BULLET, MISSILE, LASER };

[CreateAssetMenu(fileName = "New Tower", menuName = "Actos/Towers")]
public class Tower_SO : ScriptableObject
{
    public ProjectTileType towerType = ProjectTileType.BULLET;
    public string description;
    public float range;
    public float bulletPerSecond;
    public float innerRadius;
    public GameObject projectilePrefab;
}
