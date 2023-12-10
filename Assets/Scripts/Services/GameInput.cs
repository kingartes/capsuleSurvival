using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : SingletonComponent<GameInput>
{
    public event EventHandler OnShoot;
    public event EventHandler OnShootCanceled;
    public event EventHandler OnReload;
    public event EventHandler OnPickup;
    public event EventHandler<OnWeaponChangedEventArgs> OnWeaponChanged;

    private PlayerInputActions playerInputActions;
    private GameManager gameManager;
    public class OnWeaponChangedEventArgs : EventArgs
    {
        public string WeaponKey;
    }

    protected override void Awake()
    {
        base.Awake();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Shoot.performed += Shoot_performed;
        playerInputActions.Player.Shoot.canceled += Shoot_canceled;
        playerInputActions.Player.ChangeWeapon.performed += ChangeWeapon_performed;
        playerInputActions.Player.Reload.performed += Reload_performed;
        playerInputActions.Player.PickUp.performed += PickUp_performed;
    }


    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnGameOver += GameManager_OnGameOver;
    }

    private void PickUp_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPickup?.Invoke(this, EventArgs.Empty);
    }

    private void GameManager_OnGameOver(object sender, EventArgs e)
    {
        playerInputActions.Player.Disable();
    }

    private void Reload_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnReload?.Invoke(this, EventArgs.Empty);
    }

    private void ChangeWeapon_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnWeaponChanged?.Invoke(this, new OnWeaponChangedEventArgs { WeaponKey = obj.control.name });
    }

    private void Shoot_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShootCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void Shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShoot?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementNormalized()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    }

}
