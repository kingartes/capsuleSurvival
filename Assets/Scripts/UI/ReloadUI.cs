using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadUI : MonoBehaviour
{
    [SerializeField] private Image reloadProgress;

    private ReloadWeapon activeWeapon;

    public void SetActiveWeapon(ReloadWeapon activeWeapon)
    {
        this.activeWeapon = activeWeapon;
    }

    private void Update()
    {
        SetReloadProgress(activeWeapon.GetReloadProgressNormalized());
    }

    private void SetReloadProgress(float progress)
    {
        reloadProgress.fillAmount = progress;
    }
}
