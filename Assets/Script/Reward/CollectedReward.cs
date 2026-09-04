using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CollectedReward : MonoBehaviour
{
    [SerializeField] private Image rewardIcon;
    [SerializeField] private TMP_Text rewardAmount;

    private int totalAmount;

    public void SetReward(Reward reward)
    {
        rewardIcon.sprite = reward.Icon;
        rewardIcon.preserveAspect = true;

        totalAmount = reward.Amount;

        UpdateAmount();
    }

    public void AddAmount(int amount)
    {
        totalAmount += amount;

        UpdateAmount();
    }

    private void UpdateAmount()
    {
        rewardAmount.text = totalAmount.ToString();
    }
}