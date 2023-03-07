using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TDEnemy))]
public class EnemyHP : MonoBehaviour
{
    [SerializeField] int maxHP = 10;
    [Tooltip("리스폰 시 상승할 체력지수")]
    [SerializeField] int difficulty = 1;

    int currentHP = 0;

    TDEnemy enemy;

    void OnEnable()
    {
        currentHP = maxHP;
    }

    void Start()
    {
        enemy = GetComponent<TDEnemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHP--;

        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            maxHP += difficulty;
            enemy.RewardGold();
        }
    }
}
