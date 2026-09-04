using System.Collections.Generic;

using UnityEngine;

public static class UniqueRewardSelect
{
    public static Reward[] Selection(Reward[] wheelRewards, int count)
    {
        if (wheelRewards == null)
        {
            Debug.LogError("odul verilmemis");
            return new Reward[0];
        }

        if (wheelRewards.Length < count)
        {
           Debug.LogError("yeterli sayıda odul yok");
            return new Reward[0];
        }

        List<Reward> poolRewards = new List<Reward>(wheelRewards);
        Reward[] result = new Reward[count];

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, poolRewards.Count);
            
            result[i] = poolRewards[randomIndex];

            poolRewards.RemoveAt(randomIndex);
        }

        
        return result;
    }
}
