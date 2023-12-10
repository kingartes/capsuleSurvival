using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AmmoContainer))]

public class Player : MonoBehaviour, IHitable
{
    public event EventHandler OnPlayerReachedEndOfLevel;

    private Health health;
    private IPickable availablePick;
    private GameInput gameInput;
    private AmmoContainer ammoContainer;
    private PlayerWeapon weapons;

    private void Awake()
    {
        health = GetComponent<Health>();
        ammoContainer = GetComponent<AmmoContainer>();
        weapons = GetComponent<PlayerWeapon>();
    }

    private void Start()
    {
        gameInput = GameInput.Instance;
        gameInput.OnPickup += GameInput_OnPickup;
    }

    private void GameInput_OnPickup(object sender, System.EventArgs e)
    {
        if (availablePick != null)
        {
            availablePick.Pickup(this);
        }
    }

    private void OnTriggerEnter(Collider other)  
    {
        if (other.gameObject.TryGetComponent<IPickable>(out IPickable pickable))
        {
            pickable.SetAsPickable();
            availablePick = pickable;
        }
        if (other.gameObject.TryGetComponent<EndOfLevel>(out EndOfLevel endOfLevel)) {
            OnPlayerReachedEndOfLevel?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IPickable>(out IPickable pickable))
        {
            pickable.SetAsNotPickable();
            availablePick = null;
        }
    }


    public void Hit(Bullet bullet)
    {
        health.TakeDamage(bullet.GetDamageValue());
    }

    public void Heal(float healAmount)
    {
        health.HealHealth(healAmount);
    }

    public void ReplenishAmmo(AmmoType ammoType, int replenishAmount)
    {
        ammoContainer.ReplenishAmmo(ammoType, replenishAmount);
    }

    public void TakeWeapon(BaseWeapon weaponPrefab, Action pickupFinished)
    {
        if (weapons.CanPickupWeapon(weaponPrefab))
        {
            weapons.GatherWeapon(weaponPrefab);
            pickupFinished();
        }
    }

}
