using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Tower))]
public class Laser : Projectile
{
    //new void Update()
    //{
    //    base.Update();
    //    HittingTarget();
    //}

    [SerializeField]
    private LineRenderer _lineRendererComponent;

    private Tower _tower;

    private void Start()
    {
        _tower = this.GetComponent<Tower>();
    }

    public void FireLaserBeam()
    {
        //TODO: DAMAGE INPUT


        //GRAPHICS PART
        _lineRendererComponent.enabled = true;

        _lineRendererComponent.SetPosition(0, _tower.GetProjectileSpawnPoint.position);
        _lineRendererComponent.SetPosition(1, _tower.GetTowerTarget.position);
    }
}
