using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthStatusText;

    private void Start()
    {
        playerHealth.OnHealthChanged += PlayerHealth_OnTakeDamage;
        UpdateVisual();
    }

    private void PlayerHealth_OnTakeDamage(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        string healthStatus = $"{playerHealth.CurrentHealth} / {playerHealth.MaxHealth}";
        healthBar.fillAmount = playerHealth.GetNormalizedHealth();
        healthStatusText.text = healthStatus;
    }
}
