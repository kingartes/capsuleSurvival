using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;

    private ReloadWeapon weapon;

    private void Awake()
    {
        weapon = GetComponent<ReloadWeapon>();
    }

    private void Start()
    {
        weapon.OnBulletShoot += Weapon_OnBulletShoot;
        weapon.OnReloadStarted += Weapon_OnReloadStarted;
    }

    private void Weapon_OnReloadStarted(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(reloadSound, transform.position, 1);
    }

    private void Weapon_OnBulletShoot(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position, 1);
    }
}
