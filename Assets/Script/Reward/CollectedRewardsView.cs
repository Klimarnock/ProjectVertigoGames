using System.Collections.Generic;
using UnityEngine;

public class CollectedRewardsView : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private CollectedReward itemPrefab;

    private Dictionary<Sprite, CollectedReward> collectedItems =new Dictionary<Sprite, CollectedReward>();

    public void Add(Reward reward)
    {
        if (reward == null) { return; }

        Sprite icon = reward.Icon;

        
        
        if (collectedItems.TryGetValue(icon,out CollectedReward existingItem))
        {
            existingItem.AddAmount(reward.Amount);
            return;
        }

        
        
        CollectedReward newItem =Instantiate(itemPrefab, content);

        newItem.SetReward(reward);

        collectedItems.Add(icon, newItem);
    }

    public void Clear()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        collectedItems.Clear();
    }
}