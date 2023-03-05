using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDEnemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;
    [SerializeField] int lifePenalty = 1;

    Bank bank;
    
    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null)
        {
            return;
        }
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if (bank == null)
        {
            return;
        }
        bank.Withdraw(goldPenalty);
    }

    public void LooseLife()
    {
        if (bank == null)
        {
            return;
        }
        bank.LooseLife(lifePenalty);
    }
}
