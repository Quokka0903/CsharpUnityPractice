using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] TDTower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                // towerPrefab을 해당 포지션에 인스턴스로 배치
                bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
                isPlaceable = !isPlaced;
            }
        }
    }
}
