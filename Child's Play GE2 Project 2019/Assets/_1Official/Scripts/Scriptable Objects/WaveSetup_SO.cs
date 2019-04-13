using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave Setup")]
public class WaveSetup_SO : ScriptableObject
{
    //public string description;
    //public GameObject enemy;
    //public int count;
    //public float rate;
    //public bool isMixedWave = false;
    //public WaveSetup_SO waveArray;

    [System.Serializable]
    public class WaveMix
    {
        //public string description;    >>> IS IT NECESSARY? MAYBE, MAYBE NOT     
        //public float range;
        //public float bulletPerSecond;
        ////public float innerRadius;
        //public GameObject projectilePrefab;

        //public string description;
        public GameObject enemy;
        public int count;
        //public float rate;
       // public bool isMixedWave = false;
    }

    [Header("Wave Setup")]
    public WaveMix[] waveMixArray;

    //public string description;
    //public GameObject enemy;
    //public int count;
    public float rate;
    //public bool isMixedWave = false;
    

}
