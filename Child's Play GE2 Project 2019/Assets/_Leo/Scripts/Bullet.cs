using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    // Update is called once per frame
    void Update()
    {
        HittingTarget();
    }
}
