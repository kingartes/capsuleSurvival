using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoContainer : MonoBehaviour
{
    public event EventHandler OnReplenishedAmmo;

    [SerializeField] private int gunAmmo;
    [SerializeField] private int riffleAmmo;
    [SerializeField] private int shotgunAmmo;
    [SerializeField] private int rocketAmmo;

    private Dictionary<AmmoType, int> ammo;

    private void Awake()
    {
        ammo = new Dictionary<AmmoType, int> {
            { AmmoType.GunAmmo, gunAmmo },
            { AmmoType.RiffleAmmo, riffleAmmo },
            { AmmoType.ShotgunAmmo, shotgunAmmo },
            { AmmoType.RocketAmmo, rocketAmmo }
        };
    }

    public int GetAmmoAmount(AmmoType ammoType)
    {
        return ammo[ammoType];
    }

    public void DecreaseAmmo(AmmoType ammoType, int ammoCount)
    {
        ammo[ammoType] -= ammoCount;
    }

    public void ReplenishAmmo(AmmoType ammoType, int ammoCount)
    {
        ammo[ammoType] += ammoCount;
        OnReplenishedAmmo?.Invoke(this, EventArgs.Empty);
    }
}
