using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
   
    [SerializeField] private GameObject panel;           
    [SerializeField] private Image rewardImage;
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private TMP_Text subtitleText;

    [SerializeField] private Button restartButton;        

    public event Action RestartRequested;

    private void Awake()
    {
        restartButton.onClick.AddListener(RequestRestart);
        
        
        panel.SetActive(false);
    }

    private void OnDestroy()
    {
        restartButton.onClick.RemoveListener(RequestRestart);
    }

    public void Show(Reward reward)
    {
        panel.SetActive(true);
        
        
        panel.transform.SetAsLastSibling();

        rewardImage.sprite = reward.Icon;
        rewardImage.preserveAspect = true;

        titleText.text = "OH NO, A BOMB EXPLODED RIGHT IN YOUR HANDS!";
        subtitleText.text = "Revive yourself to keep your rewards.";
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    private void RequestRestart()
    {
        if(RestartRequested != null)
        {
            RestartRequested.Invoke();
        }
    }
}
