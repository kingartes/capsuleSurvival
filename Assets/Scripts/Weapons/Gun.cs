using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : ReloadWeapon
{
    private float shootDelay = 0;

    protected override void Update()
    {
        base.Update();
        shootDelay -= Time.deltaTime * rateOfFire;
    }

    public override IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelay);
        if ( CanShoot() )
        {
            BulletShoot();
            SpawnBullet(transform.forward);
            DecreaseClipAmmo();
        } else if (!IsReloading)
        {
            StartReloading();
        }
        yield return null;
        shootDelay = 1;
    }
}
