using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBar : MonoBehaviour
{
    [SerializeField] private ZoneNode[] zoneNodes;

    [Header("Current Zone")]
    [SerializeField] private Sprite currentNormalZoneSprite;
    [SerializeField] private Sprite currentSafeZoneSprite;
    [SerializeField] private Sprite currentSuperZoneSprite;
    [Header("New Zone")]
    [SerializeField] private Sprite nextNormalZoneSprite;
    [SerializeField] private Sprite nextSafeZoneSprite;
    [SerializeField] private Sprite nextSuperZoneSprite;

    public void SetZone(int currentZone)
    {
        for (int i = 0; i < zoneNodes.Length; i++)
        {
            int zoneNumber = currentZone + i;

            WheelType wheelType = WheelRules.GetWheelType(zoneNumber);

            bool isCurrentZone = i == 0;

            Sprite sprite = GetZoneSprite(wheelType, isCurrentZone);

            zoneNodes[i].SetZone(zoneNumber, sprite);


        }

    }
    private Sprite GetZoneSprite(WheelType wheelType, bool isCurrentZone)
    {
        if (isCurrentZone)
        {

            switch (wheelType)
            {
                case WheelType.Bronze:
                    return currentNormalZoneSprite;
                case WheelType.Silver:
                    return currentSafeZoneSprite;
                case WheelType.Gold:
                    return currentSuperZoneSprite;


            }
        }

        switch (wheelType)
        {
            case WheelType.Bronze:
                return nextNormalZoneSprite;
            case WheelType.Silver:
                return nextSafeZoneSprite;
            case WheelType.Gold:
                return nextSuperZoneSprite;


        }

        return nextNormalZoneSprite;
    }
}