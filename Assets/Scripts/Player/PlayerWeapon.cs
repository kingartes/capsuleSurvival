using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public struct WeaponData
    {
        public int Index;
        public Sprite Icon;
    }

    public event EventHandler OnWeaponChanged;
    public event EventHandler OnWeaponsListUpdated;

    [SerializeField]
    private List<BaseWeapon> availableWeapons;
    [SerializeField]
    private Transform weaponSlot;

    private BaseWeapon activeWeapon;
    private int activeWeaponIndex;


    private Coroutine shootingCoroutine;

    private void Awake()
    {
        activeWeapon = availableWeapons.First();
        Debug.Log(activeWeapon);
        activeWeaponIndex = 0;
    }

    private void Start()
    {
        GameInput.Instance.OnShoot += GameInput_OnShoot;
        GameInput.Instance.OnShootCanceled += GameInput_OnShootCanceled;
        GameInput.Instance.OnWeaponChanged += GameInput_OnWeaponChanged;
        GameInput.Instance.OnReload += GameInput_OnReload;
    }

    private void GameInput_OnReload(object sender, EventArgs e)
    {
        if (activeWeapon is ReloadWeapon)
        {
            ((ReloadWeapon)activeWeapon).Reload();
        }
    }

    private void GameInput_OnWeaponChanged(object sender, GameInput.OnWeaponChangedEventArgs e)
    {
        int weaponIndex = int.Parse(e.WeaponKey) - 1;
        if (activeWeaponIndex == (weaponIndex))
        {
            return;
        }
        BaseWeapon weapon = availableWeapons.ElementAtOrDefault(weaponIndex);
        if (weapon != null)
        {
            activeWeapon.gameObject.SetActive(false);
            activeWeapon = weapon;
            activeWeapon.gameObject.SetActive(true);
            activeWeaponIndex = weaponIndex;
            OnWeaponChanged?.Invoke(this, EventArgs.Empty);
        } 
    }

    private void GameInput_OnShootCanceled(object sender, System.EventArgs e)
    {
        StopCoroutine(shootingCoroutine);
    }

    private void GameInput_OnShoot(object sender, System.EventArgs e)
    {
        shootingCoroutine = StartCoroutine(activeWeapon.Shoot());
    }

    private BaseWeapon CreateWeaponInstance(BaseWeapon weaponPrefab)
    {
        BaseWeapon weapon = Instantiate(weaponPrefab, weaponSlot);
        weapon.gameObject.SetActive(false);
        return weapon;
    }


    public Sprite GetWeaponIcon(int weaponIndex)
    {
        return availableWeapons[weaponIndex]?.Icon;
    }

    public int GetWeaponIndex()
    {
        return activeWeaponIndex;
    }

    public int GetAvailableWeaponsCount()
    {
        return availableWeapons.Count;
    }

    public bool TryGetActiveReloadWeapon(out ReloadWeapon activeWeapon)
    {
        activeWeapon = null;
        if (this.activeWeapon is ReloadWeapon)
        {
            activeWeapon = this.activeWeapon as ReloadWeapon;
        }
        return activeWeapon != null;
    }

    public bool CanPickupWeapon(BaseWeapon weapon)
    {
        BaseWeapon weaponInList = availableWeapons.Find(w => w.GetType() == weapon.GetType());
        return weaponInList == null;
    }

    public void GatherWeapon(BaseWeapon weaponPrefab)
    {
        BaseWeapon weapon = CreateWeaponInstance(weaponPrefab);
        availableWeapons.Add(weapon);
        OnWeaponsListUpdated?.Invoke(this, EventArgs.Empty);
    }
}
