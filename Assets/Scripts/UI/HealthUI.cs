using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    [SerializeField] private Health health;

    private void Start()
    {
        health.OnHealthChanged += Health_OnTakeDamage;
    }

    private void Health_OnTakeDamage(object sender, System.EventArgs e)
    {
        healthBar.fillAmount = health.GetNormalizedHealth();
    }
}
