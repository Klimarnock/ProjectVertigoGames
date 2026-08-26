using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New RewardData", menuName = "RewardData")]
public class Reward : ScriptableObject
{
    [SerializeField]
    private string rewardName;
    [SerializeField]
    private RewardType type;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int amount = 1;

    public string RewardName => rewardName; public RewardType Type => type;
    public Sprite Icon => icon; public int Amount => amount;
    public bool IsDeath => type == RewardType.Death;

}
