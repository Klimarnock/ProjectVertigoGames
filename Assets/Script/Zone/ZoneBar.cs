
using TMPro;
using UnityEngine;



public class ZoneBar : MonoBehaviour
{
    [SerializeField] private ZoneNode topNode;
    [SerializeField] private ZoneNode middleNode;
    [SerializeField] private ZoneNode bottomNode;


    [SerializeField] private RectTransform currentFrame;


    [Header("Zone Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite safeSprite;
    [SerializeField] private Sprite superSprite;


    [Header("Text Colors")]
    [SerializeField] private Color normalTextColor;
    [SerializeField] private Color safeTextColor;
    [SerializeField] private Color superTextColor;


    [Header("Zone Text")]
    [SerializeField] private TMP_Text safeZoneValue;
    [SerializeField] private TMP_Text superZoneValue;



    public void SetZone(int currentZone)
    {

        Debug.Log("zone degisti" + currentZone);



        if (currentZone == 1)
        {

            SetNode(topNode, 1, true);

            SetNode(middleNode, 2, false);

            SetNode(bottomNode, 3, false);


            currentFrame.anchoredPosition = topNode.Rect.anchoredPosition;

        }

        else
        {

            SetNode(topNode, currentZone - 1, false);

            SetNode(middleNode, currentZone, true);

            SetNode(bottomNode, currentZone + 1, false);


            currentFrame.anchoredPosition = middleNode.Rect.anchoredPosition;

        }



        int nextSafeZone = GetNextSafeZone(currentZone);

        int nextSuperZone = GetNextSuperZone(currentZone);



        safeZoneValue.text = nextSafeZone.ToString();

        superZoneValue.text = nextSuperZone.ToString();

    }




    private void SetNode(ZoneNode node, int zoneNumber, bool isCurrent)
    {

        if (isCurrent)
        {

            node.SetZone(zoneNumber, normalSprite, normalTextColor);

            return;
        }



        WheelType wheelType = WheelRules.GetWheelType(zoneNumber);



        if (wheelType == WheelType.Bronze)
        {

            node.SetZone(zoneNumber, normalSprite, normalTextColor);

        }

        else if (wheelType == WheelType.Silver)
        {

            node.SetZone(zoneNumber, safeSprite, safeTextColor);

        }

        else
        {

            node.SetZone(zoneNumber, superSprite, superTextColor);

        }

    }




    private int GetNextSafeZone(int currentZone)
    {

        int zone = currentZone + 1;


        while (zone % 5 != 0 || zone % 30 == 0)
        {

            zone++;

        }


        return zone;
    }




    private int GetNextSuperZone(int currentZone)
    {

        int zone = currentZone + 1;


        while (zone % 30 != 0)
        {

            zone++;

        }


        return zone;
    }
}