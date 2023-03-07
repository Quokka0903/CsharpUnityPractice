using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 15;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 5f;
    Random randomObj = new Random();

    GameObject[] pool;
    int randomValue;

    void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());   
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // 현재 사용하는 기능: 개체(enemyPrefab)를 인스턴스화하고 새 개체에 부모(transform) 설정
            EnableObjectInPool();
            randomValue = randomObj.Next(1, 10);
            yield return new WaitForSeconds(spawnTimer / randomValue);
        }
    }

 
}
