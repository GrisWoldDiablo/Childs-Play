using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseClass : MonoBehaviour
{
    [SerializeField] private int hitPoints;
    
    public int HitPoints { get => hitPoints; set => hitPoints = value; }
}
