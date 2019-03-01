using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave Setup")]
public class WaveSetup_SO : ScriptableObject
{
    public string description; 
    public GameObject enemy;
    public int count;
    public float rate;
}
