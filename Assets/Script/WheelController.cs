using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
public class WheelController : MonoBehaviour
    {

        [SerializeField] private int turncount;
        [SerializeField] private Reward[] bronzeRewards;
        [SerializeField] private Reward[] silverRewards;
        [SerializeField] private Reward[] goldRewards;
        [SerializeField] private Reward deathReward;
    [SerializeField] private Wheel_Vision wheelvision;
    

    private const int slotcount = 8;
    private List<Reward> collectedReward = new List<Reward>();
        private Reward[] turnRewards = new Reward[slotcount];
    
        private Reward[] bronzeWheel()
        {
            List<Reward> rewards = new List<Reward>();
            List<Reward> rewardList = new List<Reward>(bronzeRewards);


            int indexDeath = Random.Range(0, 8);


            for (int i = 0; i < slotcount; i++)
            {
                if (i == indexDeath)
                {
                    rewards.Add(deathReward);
                }
                else
                {
                    int randomIndex = Random.Range(0, rewardList.Count);
                    rewards.Add(rewardList[randomIndex]);
                    rewardList.RemoveAt(randomIndex);
                }


            }
            return rewards.ToArray();

        }

        private Reward[] silverWheel()
        {
            List<Reward> rewards = new List<Reward>();
            List<Reward> rewardList = new List<Reward>(silverRewards);

            for (int i = 0; i < slotcount; i++)
            {
                int randomIndex = Random.Range(0, rewardList.Count);
                rewards.Add(rewardList[randomIndex]);
                rewardList.RemoveAt(randomIndex);
            }
            return rewards.ToArray();

        }
        private Reward[] goldWheel()
        {
            List<Reward> rewards = new List<Reward>();
            List<Reward> rewardList = new List<Reward>(goldRewards);
            for (int i = 0; i < slotcount; i++)
            {
                int randomIndex = Random.Range(0, rewardList.Count);
                rewards.Add(rewardList[randomIndex]);
                rewardList.RemoveAt(randomIndex);
            }
            return rewards.ToArray();
    }
    private void TurnWheel()
    {
        int zoneNumber = turncount + 1;


        WheelType wheelType = WheelRules.GetWheelType(zoneNumber);
        
        switch (wheelType)
        {
            case WheelType.Bronze:
                turnRewards = bronzeWheel();
                break;
            case WheelType.Silver:
                turnRewards = silverWheel();
                break;
            case WheelType.Gold:
                turnRewards = goldWheel();
                break;
        }
        

        wheelvision.SetZoneInfo(zoneNumber, wheelType);
        wheelvision.SetZone(zoneNumber);
        wheelvision.SetWheelBaseImage(wheelType);
    }
    private void Start()
        {
            TurnWheel();
            wheelvision.SetRewardImages(turnRewards);
        }
        private void OnEnable()
    {
        wheelvision.OnSpinCompleted += HandleSpinCompleted;
        wheelvision.OnRestartRequested += RestartGame;
        wheelvision.OnStopRequested += StopGame;
        wheelvision.OnRewardAnimationCompleted += NextWheel;    
    }
    private void OnDisable()
    {
        wheelvision.OnSpinCompleted -= HandleSpinCompleted;
        wheelvision.OnRestartRequested -= RestartGame;
        wheelvision.OnStopRequested -= StopGame;
        wheelvision.OnRewardAnimationCompleted -= NextWheel;
    }

    private void RestartGame()
    {
        collectedReward.Clear();
        turncount = 0;
        wheelvision.ClearCollectedRewards();
        TurnWheel();
        wheelvision.SetRewardImages(turnRewards);
        wheelvision.HideGameOverPanel();
        wheelvision.SetWheelButtonInteractable(true);
        Debug.Log("Game restarted.");
    }
    private void StopGame()
    {
        foreach(Reward reward in collectedReward)
        {
            Debug.Log($"Collected: {reward.RewardName}");
        }
        Debug.Log("Game stopped.");
        wheelvision.SetWheelButtonInteractable(false);
    }

    private void HandleSpinCompleted(int rewardIndex)
    {

        Reward reward = turnRewards[rewardIndex];
        Debug.Log($"Spin completed. Reward: {reward.RewardName}");
        if (reward.IsDeath)
        {
            collectedReward.Clear();
          

            wheelvision.ClearCollectedRewards();
            wheelvision.DeathAnimation(reward);
            

            return;
        }
        collectedReward.Add(reward);


        Debug.Log($"Collected count: {collectedReward.Count}");

        foreach (Reward item in collectedReward)
        {
            Debug.Log($"Collected: {item.RewardName}");
        }
        turncount++;
        wheelvision.ShowRewardAnimation(reward, collectedReward.Count);
    }

    private void NextWheel()
    {
        TurnWheel();
        wheelvision.SetRewardImages(turnRewards);
        WriteCollectedRewards();
        wheelvision.SetWheelButtonInteractable(true);
    }

    private void WriteCollectedRewards()
    {
        Debug.Log("-------------------------------------");
        Debug.Log("Collected Rewards:");
        foreach (var reward in collectedReward)
        {
            Debug.Log($"- {reward.RewardName}");
        }
        Debug.Log("-------------------------------------");

    }
}
