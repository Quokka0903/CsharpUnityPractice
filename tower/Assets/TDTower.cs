using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDTower : MonoBehaviour
{

    [SerializeField] int cost = 30;

    public bool CreateTower(TDTower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost);
            cost *= 2;
            return true;
        }

        return false;
    }
    
}
