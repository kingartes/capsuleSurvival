using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsItemUI : MonoBehaviour
{
    [SerializeField] private Image Icon;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color inactiveTextColor;

    public void SetIcon(Sprite icon)
    {
        this.Icon.sprite = icon;
    }

    public void MakeActive()
    {
        backgroundImage.GetComponent<Outline>().effectColor = activeColor;
    }

    public void MakeInactive()
    {
        backgroundImage.GetComponent <Outline>().effectColor = inactiveColor;
    }
}
