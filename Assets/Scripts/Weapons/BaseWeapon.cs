using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public event EventHandler OnBulletShoot;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float projectileSpeed = 200;
    [SerializeField] protected float rateOfFire = 0.1f;
    [SerializeField] private float damage;
    [SerializeField] protected AmmoSO ammoSO;
    [SerializeField] private Sprite icon;

    public Sprite Icon => icon;

    public abstract IEnumerator Shoot();

    protected abstract bool CanShoot();

    protected void SpawnBullet(Vector3 shootDirection)
    {
        Bullet projectile = Instantiate(ammoSO.bullet, shootingPoint);
        projectile.Init(damage);
        projectile.transform.SetParent(null);
        projectile.gameObject.SetActive(true);
        projectile.GetComponent<Rigidbody>().velocity = shootDirection * projectileSpeed;
    }

    protected void BulletShoot()
    {
        OnBulletShoot?.Invoke(this, EventArgs.Empty);
    }
}
