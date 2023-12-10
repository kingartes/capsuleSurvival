using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRiffle : ReloadWeapon
{
    public override IEnumerator Shoot()
    {
        while(true)
        {
            if (CanShoot())
            {
                BulletShoot();
                SpawnBullet(transform.forward);
                DecreaseClipAmmo();
            }
            else if (!IsReloading)
            {
                StartReloading();
            }
            yield return new WaitForSeconds(1/rateOfFire);
        }
    }
}
