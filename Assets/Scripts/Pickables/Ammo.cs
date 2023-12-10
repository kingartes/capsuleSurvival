using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IPickable
{
    [SerializeField] private int ammoAmount;
    [SerializeField] private PickUpUI PickUpUI;
    [SerializeField] private AmmoSO ammoSO;

    public void Pickup(Player player)
    {
        player.ReplenishAmmo(ammoSO.ammoType, ammoAmount);
        Destroy(gameObject);
    }

    public void SetAsPickable()
    {
        PickUpUI.gameObject.SetActive(true);
    }

    public void SetAsNotPickable()
    {
        PickUpUI.gameObject.SetActive(false);
    }
}
