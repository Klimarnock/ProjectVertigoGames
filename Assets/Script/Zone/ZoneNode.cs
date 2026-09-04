using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class ZoneNode : MonoBehaviour
{

    [SerializeField] private Image background;

    [SerializeField] private TMP_Text valueText;




    public RectTransform Rect
    {
        get
        {
            return transform as RectTransform;
        }
    }




    public void SetZone(int zoneNumber, Sprite sprite, Color textColor)
    {

        background.sprite = sprite;


        valueText.text = zoneNumber.ToString();

        valueText.color = textColor;

    }

}