using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityRewardsDropList : MonoBehaviour
{
    public List<AbilityReward> abilityRewards;

    public List<AbilityReward> bossDrops1;
    public List<AbilityReward> bossDrops2;
    public List<AbilityReward> bossDrops3;
    public List<AbilityReward> bossDrops4;
    public List<AbilityReward> bossDrops5;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
			
	}

    public List<AbilityReward> GetAbilityRewards() {
        return abilityRewards;
    }

    public void DropAbility() {
        if (!Global.bossMedley) {
            AbilityReward droppedAbility = abilityRewards[Random.Range(0, abilityRewards.Count - 1)];
            Instantiate(droppedAbility, new Vector3(0, 0, 0), Quaternion.identity);
            abilityRewards.Remove(droppedAbility);
        }

    }

    public void AddBossDropsToList(int stage) {
        if (stage == 1) {
            foreach (AbilityReward ab in bossDrops1) {
                if (!abilityRewards.Contains(ab)) {
                    abilityRewards.Add(ab);
                }
            }
        }
        else if (stage == 2)
        {
            foreach (AbilityReward ab in bossDrops2)
            {
                if (!abilityRewards.Contains(ab))
                {
                    abilityRewards.Add(ab);
                }
            }
        }
        else if (stage == 3)
        {
            foreach (AbilityReward ab in bossDrops3)
            {
                if (!abilityRewards.Contains(ab))
                {
                    abilityRewards.Add(ab);
                }
            }
            foreach (AbilityReward ab in bossDrops1)
            {
                if (abilityRewards.Contains(ab)) {
                    abilityRewards.Remove(ab);
                }
            }
        }
        else if (stage == 4)
        {
            foreach (AbilityReward ab in bossDrops4)
            {
                if (!abilityRewards.Contains(ab))
                {
                    abilityRewards.Add(ab);
                }
            }
            foreach (AbilityReward ab in bossDrops2)
            {
                if (abilityRewards.Contains(ab))
                {
                    abilityRewards.Remove(ab);
                }
            }
        }
        else if (stage == 5)
        {
            foreach (AbilityReward ab in bossDrops5)
            {
                if (!abilityRewards.Contains(ab))
                {
                    abilityRewards.Add(ab);
                }
            }
            foreach (AbilityReward ab in bossDrops3)
            {
                if (abilityRewards.Contains(ab))
                {
                    abilityRewards.Remove(ab);
                }
            }
        }
    }
}
