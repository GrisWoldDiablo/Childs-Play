using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectTileType { BULLET, MISSILE, LASER };

[CreateAssetMenu(fileName = "New Tower", menuName = "Actos/Towers")]
public class Tower_SO : ScriptableObject
{
    [Header ("Current Tower Stats")]
    public ProjectTileType towerType = ProjectTileType.BULLET;
    public string description;
    public int towerLevel = 1;
    public float range;
    public float bulletPerSecond;
    public float innerRadius;
    public GameObject projectilePrefab;


    [System.Serializable]
    public class TowerLevels
    {
        //public string description;    >>> IS IT NECESSARY? MAYBE, MAYBE NOT     
        public float range;
        public float bulletPerSecond;
        //public float innerRadius;
        public GameObject projectilePrefab;
    }

    [Header("Tower Levels")]
    public TowerLevels[] TowerLevelsArray;


    public void SetTowerLevel(int lvl)
    {
        towerLevel = lvl;

        int levelIndex = lvl - 1;

        range = TowerLevelsArray[levelIndex].range;
        bulletPerSecond = TowerLevelsArray[levelIndex].bulletPerSecond;
    }
}
