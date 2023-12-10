using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoStatus;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private ReloadUI reloadUI;
   

    private ReloadWeapon activeWeapon;
    private bool isReloadWeaponEquiped = false;

    private void Start()
    {
        playerWeapon.OnWeaponChanged += PlayerWeapon_OnWeaponChanged;
        isReloadWeaponEquiped = playerWeapon.TryGetActiveReloadWeapon(out activeWeapon);
        if (isReloadWeaponEquiped)
        {
            UpdateAmmoVisual();
            activeWeapon.OnClipAmmoChanged += ActiveWeapon_OnClipAmmoChanged;
            activeWeapon.OnReloadStarted += ActiveWeapon_OnReloadStarted;
            activeWeapon.OnReloadFinished += ActiveWeapon_OnReloadFinished;
        }
    }

    private void PlayerWeapon_OnWeaponChanged(object sender, System.EventArgs e)
    {
      
        if (activeWeapon != null)
        {
            activeWeapon.OnClipAmmoChanged -= ActiveWeapon_OnClipAmmoChanged;
            activeWeapon.OnReloadStarted -= ActiveWeapon_OnReloadStarted;
            activeWeapon.OnReloadFinished -= ActiveWeapon_OnReloadFinished;

        }
        isReloadWeaponEquiped = playerWeapon.TryGetActiveReloadWeapon(out activeWeapon);
        if (isReloadWeaponEquiped)
        {
            UpdateAmmoVisual();
            activeWeapon.OnClipAmmoChanged += ActiveWeapon_OnClipAmmoChanged;
            activeWeapon.OnReloadStarted += ActiveWeapon_OnReloadStarted;
            activeWeapon.OnReloadFinished += ActiveWeapon_OnReloadFinished;
        } 
    }

    private void ActiveWeapon_OnReloadFinished(object sender, EventArgs e)
    {
        UpdateAmmoVisual();
        reloadUI.gameObject.SetActive(false); ;
    }

    private void ActiveWeapon_OnReloadStarted(object sender, EventArgs e)
    {
        reloadUI.SetActiveWeapon(activeWeapon);
        reloadUI.gameObject.SetActive(true);
    }

    private void ActiveWeapon_OnClipAmmoChanged(object sender, System.EventArgs e)
    {
        UpdateAmmoVisual();
    }

    private void UpdateAmmoVisual()
    {
        if (isReloadWeaponEquiped)
        {
            reloadUI.gameObject.SetActive(activeWeapon.IsReloading);
            reloadUI.SetActiveWeapon(activeWeapon);
            ammoStatus.gameObject.SetActive(true);
            string ammoText = BuildAmmoStatusText();
            ammoStatus.text = ammoText;
        } else
        {
            ammoStatus.gameObject.SetActive(false);
        }
    }

    private string BuildAmmoStatusText()
    {
        int currentClipSize = activeWeapon.CurrentClipAmmo;
        int totalAmmo = activeWeapon.TotalAmmo;
        return $"{currentClipSize} / {totalAmmo}";
    }
}
