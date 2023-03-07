using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TDEnemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)]float speed = 0.5f;

    List<Node> path = new List<Node>();
    TDEnemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    void Awake()
    {
        enemy = GetComponent<TDEnemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        // InvokeRepeating("FollowPath", 0, 1f);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine (FollowPath());
        // GameObject parent = GameObject.FindGameObjectWithTag("Path");

        // foreach (Transform child in parent.transform)
        // {
        //     Tile tile = child.GetComponent<Tile>();
        //     if (tile != null)
        //     {
        //         path.Add(tile);
        //     }
        // }
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.LooseLife();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition =  gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }   

        FinishPath();
    }
}
