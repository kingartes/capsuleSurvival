using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerWeapon;

public class WeaponsUI : MonoBehaviour
{
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private Transform weaponItemUI;

    private List<WeaponsItemUI> weaponItemsUI;

    private void DrawWeaponsList()
    {
        foreach (Transform child in transform)
        {
            if (child == weaponItemUI) continue;
            Destroy(child.gameObject);
        }
        int weaponsCount = playerWeapon.GetAvailableWeaponsCount();
        weaponItemsUI = new List<WeaponsItemUI>();
        for (int i = 0; i < weaponsCount; i++)
        {
            DrawWeaponIconUI(i);
        }
        DrawActiveItem();
    }

    private void Start()
    {
        DrawWeaponsList();
        playerWeapon.OnWeaponChanged += PlayerWeapon_OnWeaponChanged;
        playerWeapon.OnWeaponsListUpdated += PlayerWeapon_OnWeaponsListUpdated;
    }

    private void PlayerWeapon_OnWeaponsListUpdated(object sender, System.EventArgs e)
    {
        DrawWeaponsList();
    }

    private void PlayerWeapon_OnWeaponChanged(object sender, System.EventArgs e)
    {
        DrawActiveItem();
    }

    private void DrawActiveItem()
    {
        int weaponIndex = playerWeapon.GetWeaponIndex();
        for (int i = 0; i < weaponItemsUI.Count; i++)
        {
            if (i == weaponIndex)
            {
                weaponItemsUI[i].MakeActive();
            }
            else
            {
                weaponItemsUI[i].MakeInactive();
            }
        }
    }

    private void DrawWeaponIconUI(int weaponUiIndex)
    {
        Transform weaponItemTransform = Instantiate(weaponItemUI, transform);
        WeaponsItemUI weaponItem = weaponItemTransform.GetComponent<WeaponsItemUI>();
        int weaponIndex = playerWeapon.GetWeaponIndex();
        Sprite weaponIcon = playerWeapon.GetWeaponIcon(weaponUiIndex);
        weaponItem.gameObject.SetActive(true);
        weaponItem.SetIcon(weaponIcon);
        if (weaponIndex == weaponUiIndex)
        {
            weaponItem.MakeActive();
        } else {
            weaponItem.MakeInactive();
        }
        weaponItemsUI.Add(weaponItem);
    }
}
