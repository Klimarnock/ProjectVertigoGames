using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private int slotCount=8;

[SerializeField] private int turnCount;

    [Header("Reward Pool")]
    [SerializeField] private Reward[] bronzeRewards;
    [SerializeField] private Reward[] silverRewards;
    [SerializeField] private Reward[] goldRewards;
    [SerializeField] private Reward deathReward;

    [Header("Wheel Vision")]
    [SerializeField] private Wheel_Vision wheelVision;


    private Reward[] turnRewards;
    private Reward shownReward;


    private void OnEnable()
    {
        if (wheelVision == null) { return; }


        wheelVision.OnSpinCompleted += TurnSpinCompleted;
        wheelVision.OnRewardCollected += ShownRewardCollected;
        wheelVision.OnRestartRequested += RestartGame;
        wheelVision.OnStopRequested += StopGame;
    }

    private void OnDisable()
    {
        
        wheelVision.OnSpinCompleted -= TurnSpinCompleted;
        wheelVision.OnRewardCollected -= ShownRewardCollected  ;
        wheelVision.OnRestartRequested -= RestartGame;
        wheelVision.OnStopRequested -= StopGame;
    }



    private void Start()
    {
        PrepareWheel();
    }



    private void PrepareWheel()
    {
     

        shownReward = null;

        int zoneNumber = turnCount + 1;
        WheelType wheelType = WheelRules.GetWheelType(zoneNumber);
        
        Reward[] rewardPool = GetRewardPool(wheelType);
        

        turnRewards = WheelRewardRules.CreateWheel(wheelType,rewardPool,deathReward,slotCount);

        if (turnRewards == null || turnRewards.Length != slotCount)
        {
           
            wheelVision.ButtonInteractable(false);
            return;
        }

        wheelVision.SetRewardImages(turnRewards);

        wheelVision.SetZone(zoneNumber, wheelType);
        wheelVision.SetSlotCount(slotCount);
        wheelVision.ButtonInteractable(true);
    }

    private Reward[] GetRewardPool(WheelType wheelType)
    {
        if (wheelType == WheelType.Bronze)
        { return bronzeRewards; }
        
        else if (wheelType == WheelType.Silver)
        { return silverRewards; }

        else { return goldRewards; }
       
        
    }

    private void TurnSpinCompleted(int rewardIndex)
    {
        if (turnRewards == null)
        {
            return;
        }

        Reward reward = turnRewards[rewardIndex];
        

        if (reward.IsDeath)
        {
            shownReward = null;

            wheelVision.ClearCollectedRewards();

            wheelVision.ShowDeathAnimation(reward);
            return;
        }

        shownReward = reward;
        wheelVision.ShowRewardAnimation(reward);
    }

    private void ShownRewardCollected(Reward reward)
    {
        
        
        wheelVision.AddCollectedReward(shownReward);

        shownReward = null;
        turnCount++;

        PrepareWheel();
    }

    private void RestartGame()
    {
        
        shownReward = null;
        turnCount = 0;

        wheelVision.ClearCollectedRewards();
        
        wheelVision.HideGameOverPanel();
        
        PrepareWheel();
    }

    private void StopGame()
    {
        wheelVision.ButtonInteractable(false);
    }
}
