using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInGJ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_WEBGL
        GamejoltManager.GetInstance().ShowLogin(); 
#endif
    }
}
