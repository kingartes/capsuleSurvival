using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnHealthDropsZero;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField] private float maxHealth;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"Damage: {damage}");
        currentHealth -= damage;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        if (currentHealth <= 0) {
            OnHealthDropsZero?.Invoke(this, EventArgs.Empty);
        }
    }

    public void HealHealth(float healAmount)
    {
        float healedHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        currentHealth = healedHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetNormalizedHealth()
    {
        return currentHealth / maxHealth;
    }
}
