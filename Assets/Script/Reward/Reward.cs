using UnityEngine;





[CreateAssetMenu(fileName = "New Reward", menuName = "CreateReward")]



public class Reward : ScriptableObject
{
    [SerializeField] private string rewardName;

    [SerializeField] private RewardType type;

    [SerializeField] private Sprite icon;

    [SerializeField] private int amount = 1;

    public string RewardName { get { return rewardName; } }

    public RewardType Type { get { return type; } }
    public Sprite Icon { get { return icon; } }
    public int Amount { get { return amount; } }
    public bool IsDeath { get { return type == RewardType.Death; } }
}
