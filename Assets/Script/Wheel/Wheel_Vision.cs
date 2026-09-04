using System;
using UnityEngine;
using UnityEngine.UI;

public class Wheel_Vision : MonoBehaviour
{
    [Header("Views")]
    [SerializeField] private WheelSpinView spinView;
    [SerializeField] private RewardAnimationView rewardAnimationView;
    [SerializeField] private GameOverView gameOverView;
    
    
    [SerializeField] private CollectedRewardsView collectedRewardsView;
    [SerializeField] private WheelZoneView zoneView;

    [Header("ButtoN")]
    [SerializeField] private Button leaveButton;

    [Header("Wheel Rewards")]
    [SerializeField] private Image[] rewardImages;
    [SerializeField] private Text[] rewardAmounts;

    public event Action<int> OnSpinCompleted;
    
    public event Action<Reward> OnRewardCollected;
    
    
    
    public event Action OnRestartRequested;
    
    public event Action OnStopRequested;

    

    private void OnEnable()
    {


        if (spinView != null)
        { spinView.SpinCompleted += SpinCompleted; }

        if (rewardAnimationView != null)
        { rewardAnimationView.CollectRequested += RewardCollected; }



        if (gameOverView != null)
        { gameOverView.RestartRequested += RestartRequested; }

        if (leaveButton != null)
        { leaveButton.onClick.AddListener(RequestStop); }
    }

    private void OnDisable()
    {
        if (spinView != null)
            spinView.SpinCompleted -= SpinCompleted;

        if (rewardAnimationView != null)
            rewardAnimationView.CollectRequested -= RewardCollected;

        if (gameOverView != null)
            gameOverView.RestartRequested -= RestartRequested;

        if (leaveButton != null)
            leaveButton.onClick.RemoveListener(RequestStop);
    }

    public void SetSlotCount(int slotCount)
    {
        spinView.SetSlotCount(slotCount);
    }



    public void SetRewardImages(Reward[] rewards)
    {
        
       

        for (int i = 0; i < rewards.Length; i++)
        {
            if (rewardImages[i] != null)
            {
                rewardImages[i].sprite = rewards[i].Icon;
                rewardImages[i].preserveAspect = true;
            }

            if (rewardAmounts[i] != null)
            { rewardAmounts[i].text = rewards[i].Amount.ToString(); }
        }
    }

   
    
    
    
    public void ButtonInteractable(bool interactable)
    {
        spinView.SetInteractable(interactable);
        
       leaveButton.interactable = interactable;




    }

    public void ShowRewardAnimation(Reward reward)
    {
        ButtonInteractable(false);

       rewardAnimationView.Show(reward);
    }

    public void AddCollectedReward(Reward reward)
    {
            collectedRewardsView.Add(reward);
    }

    public void ShowDeathAnimation(Reward reward)
    {
        ButtonInteractable(false);

      
            gameOverView.Show(reward);
    }

    public void HideGameOverPanel()
    {
       
            gameOverView.Hide();
    }

    public void ClearCollectedRewards()
    {
        
            collectedRewardsView.Clear();
    }

    public void SetZone(int zoneNumber, WheelType wheelType) { 
            zoneView.SetZone(zoneNumber, wheelType);
            

    }

    private void SpinCompleted(int rewardIndex)
    {
        if(OnSpinCompleted != null)
        {
            OnSpinCompleted.Invoke(rewardIndex);    
        }
    }




    private void RewardCollected(Reward reward)
    {
        if(OnRewardCollected != null)
        {
            OnRewardCollected.Invoke(reward);
        }
    }

    private void RestartRequested()
    {
        if(OnRestartRequested != null)
        {
            OnRestartRequested.Invoke();
        }
    }

    private void RequestStop()
    {
        if(OnStopRequested != null)
        {
            OnStopRequested.Invoke();
        }
    }
}
