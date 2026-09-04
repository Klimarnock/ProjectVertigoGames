using System;
using UnityEngine;
using Random = UnityEngine.Random;
public static class WheelRewardRules
{
    public static Reward[] CreateWheel(WheelType wheelType,Reward[] wheelRewards,Reward deathReward,int slotCount)
    {

        if (wheelType != WheelType.Bronze)
        {
            return UniqueRewardSelect.Selection(wheelRewards, slotCount);
        }
        else
        {

            if (deathReward == null)
            {
                Debug.Log("death yok");
                return new Reward[0];
            }



            Reward[] withoutDeathRewards =UniqueRewardSelect.Selection(wheelRewards, slotCount - 1);


           


            Reward[] bronzeWheelRewards  = new Reward[slotCount];

            int deathIndex = Random.Range(0, slotCount);
            int index = 0;


            for (int i = 0; i < slotCount; i++)
            {
                if (i == deathIndex)
                {
                    bronzeWheelRewards[i] = deathReward;
                }
                else
                {
                    bronzeWheelRewards[i] = withoutDeathRewards[index];
                    index++;
                }
            }


            return bronzeWheelRewards;

        }
    }
    }