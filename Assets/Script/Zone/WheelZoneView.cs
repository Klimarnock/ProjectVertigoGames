using UnityEngine;
using UnityEngine.UI;

public class WheelZoneView : MonoBehaviour
{
    [Header("Zone")]
    [SerializeField] private ZoneBar zoneBar;

    [Header("Wheel")]
    [SerializeField] private Image wheelBaseImage;
    [SerializeField] private Image wheelIndicatorImage;

    [Header("Bronze")]
    [SerializeField] private Sprite bronzeBaseSprite;
    [SerializeField] private Sprite bronzeIndicatorSprite;

    [Header("Silver")]
    [SerializeField] private Sprite silverBaseSprite;
    [SerializeField] private Sprite silverIndicatorSprite;

    [Header("Gold")]
    [SerializeField] private Sprite goldBaseSprite;
    [SerializeField] private Sprite goldIndicatorSprite;

 
  
    public void SetZone(int zoneNumber, WheelType wheelType)
    {

            zoneBar.SetZone(zoneNumber);

        SetWheelTheme(wheelType);
    }

    private void SetWheelTheme(WheelType wheelType)
    {
       
        if (wheelType == WheelType.Silver)
        {
            
        wheelBaseImage.sprite = silverBaseSprite;
        wheelIndicatorImage.sprite = silverIndicatorSprite;
        }
        else if (wheelType == WheelType.Gold)
        {
            
                wheelBaseImage.sprite = goldBaseSprite;
             wheelIndicatorImage.sprite = goldIndicatorSprite;
        }
        else
        {
          
                wheelBaseImage.sprite = bronzeBaseSprite;
                 wheelIndicatorImage.sprite = bronzeIndicatorSprite;
        }
    }

   
}
