using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReloadWeapon : BaseWeapon
{
    public event EventHandler OnClipAmmoChanged;
    public event EventHandler OnReloadStarted;
    public event EventHandler OnReloadFinished;
   

    public int CurrentClipAmmo => currentClipAmmo;
    public int TotalAmmo => ammoContainer.GetAmmoAmount(ammoSO.ammoType);
    public bool IsReloading => isReloading;


    [SerializeField] protected int clipSize;
    [SerializeField] protected int reloadTime;


    private float reloadTimer = 0;
 
    public bool isReloading = false;

    private int currentClipAmmo;

    private AmmoContainer ammoContainer;

    private void Awake()
    {
        currentClipAmmo = clipSize;
        ammoContainer = gameObject.GetComponentInParent<AmmoContainer>();
    }

    private void Start()
    {
        ammoContainer.OnReplenishedAmmo += AmmoContainer_OnReplenishedAmmo;
    }

    private void AmmoContainer_OnReplenishedAmmo(object sender, EventArgs e)
    {
        OnClipAmmoChanged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void Update()
    {
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > reloadTime)
            {
                currentClipAmmo = clipSize;
                StopReloading();
            }
        }
    }

    protected override bool CanShoot()
    {
        return !IsReloading && HasAmmoInClip();
    }

    public void Reload()
    {
        if (currentClipAmmo < clipSize && ammoContainer.GetAmmoAmount(ammoSO.ammoType) >= clipSize )
        {
            StartReloading();
        }
    }

    protected void StartReloading()
    {
        isReloading = true;
        OnReloadStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void StopReloading()
    {
        isReloading = false;
        reloadTimer = 0;
        ammoContainer.DecreaseAmmo(ammoSO.ammoType, clipSize);
        OnClipAmmoChanged?.Invoke(this, EventArgs.Empty);
        OnReloadFinished?.Invoke(this, EventArgs.Empty);
    }

    public bool HasAmmoInClip()
    {
        return currentClipAmmo > 0;
    }

    public void DecreaseClipAmmo()
    {
        currentClipAmmo--;
        OnClipAmmoChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetReloadProgressNormalized()
    {
        return (reloadTime - reloadTimer) / reloadTime;
    }

    public AmmoSO GetAmmoSO()
    {
        return ammoSO;
    }
}
