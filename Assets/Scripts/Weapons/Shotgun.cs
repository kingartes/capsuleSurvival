using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : ReloadWeapon
{
    [SerializeField] private float shootDegree;
    [SerializeField] private int projectileCount;

    private float shootDelay = 0;

    protected override void Update()
    {
        base.Update();
        shootDelay -= Time.deltaTime * rateOfFire;
    }

    public override IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelay);
        if (CanShoot())
        {
            List<Vector3> shootDirections = VectorUtils.GetVectorsInCone(transform.forward, shootDegree, projectileCount);
            BulletShoot();
            foreach (Vector3 shootDirection in shootDirections)
            {
                SpawnBullet(shootDirection);
            }
            DecreaseClipAmmo();
        }
        else if (!IsReloading)
        {
            StartReloading();
        }
        yield return null;
        shootDelay = 1;
    }


}
