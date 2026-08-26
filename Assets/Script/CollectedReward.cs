using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CollectedReward : MonoBehaviour
{
    [SerializeField] private TMP_Text rewardname_value;

    [SerializeField] private Image reward_icon;
    [SerializeField] private TMP_Text reward_count_value;
    private Reward reward;
    private Reward Reward => reward;
    private int count;
    public void SetReward(Reward newReward)
    {
        reward = newReward;
        count = 1;

        reward_icon.sprite = newReward.Icon;
        reward_icon.preserveAspect = true;

        rewardname_value.text = newReward.RewardName.Replace("_"," ");

        UpdateCount();
    }
    public void AddCount()
    {
        count++;
        UpdateCount();



    }


    private void UpdateCount()
    {
        reward_count_value.text = "x" + count ;
    }
}