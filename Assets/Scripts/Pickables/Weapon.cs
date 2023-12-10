using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IPickable
{
    [SerializeField] private WeaponSO weaponSo;
    [SerializeField] private PickUpUI PickUpUI;



    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void Pickup(Player player)
    {
        player.TakeWeapon(weaponSo.weaponPrefab, DestroySelf);
    }

    public void SetAsNotPickable()
    {
        PickUpUI.gameObject.SetActive(false);
    }

    public void SetAsPickable()
    {
        PickUpUI.gameObject.SetActive(true);
    }
}
