using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] GameObject laser;
	[SerializeField] float maxSpeed = 15.0f;
    [SerializeField] float speedNow;
    [SerializeField] float rotateSpeed = 10.0f;       // 회전 속도

    float h, v;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessFiring();
    }

    // 이동 관련 함수를 짤 때는 Update보다 FixedUpdate가 더 효율이 좋다고 한다. 그래서 사용했다.
    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v); // new Vector3(h, 0, v)가 자주 쓰이게 되었으므로 dir이라는 변수에 넣고 향후 편하게 사용할 수 있게 함

        // 바라보는 방향으로 회전 후 다시 정면을 바라보는 현상을 막기 위해 설정
        if (!(h == 0 && v == 0))
        {
            GridManager gridManager = FindObjectOfType<GridManager>();
            if (gridManager != null)
            {

                try
                {
                    Vector2Int steppedTile = gridManager.GetCoordinatesFromPosition(transform.position);

                    if (steppedTile == null)
                    {
                    speedNow = -maxSpeed;
                    }
                    if (!gridManager.GetNode(steppedTile).isWalkable)
                    {
                        speedNow = maxSpeed / 2;
                    }
                    else
                    {
                        speedNow = maxSpeed;
                    }

                }
                catch
                {
                    transform.position -= dir * 3;
                }

            }
            

            // 이동과 회전을 함께 처리
            transform.position += dir * speedNow * Time.deltaTime;
            // 회전하는 부분. Point 1.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }
    }

    void ProcessFiring() {
        // 스페이스 누르면 발싸
        if (Input.GetButton("Jump")) {
            Debug.Log("Firing");
            SetLaserActive(true);
        } else {
            SetLaserActive(false);
        }
    }

    void SetLaserActive(bool isActive) 
    {    
        var emissionModule = laser.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
        if (isActive) 
        {
            Debug.Log(emissionModule);
            Debug.Log(emissionModule.enabled);
        }

    }

}
