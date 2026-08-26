using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;
using UnityEngine.UI;
public class ZoneNode : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text zoneValue;

    public void SetZone(int zoneNumber, Sprite backgroundSprite)
    {
        zoneValue.text = zoneNumber.ToString();
        backgroundImage.sprite = backgroundSprite;
    }
}
