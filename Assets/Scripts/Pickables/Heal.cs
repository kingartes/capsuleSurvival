using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, IPickable 
{
    [SerializeField] private float healAmount;
    [SerializeField] private PickUpUI PickUpUI;

    public void Pickup(Player player)
    {
        player.Heal(healAmount);
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
