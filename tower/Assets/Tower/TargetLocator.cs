using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 25f;
    Transform target;

    // Start is called before the first frame update
    // void Start()
    // {
    //     target = FindObjectOfType<Enemies>().transform;
    // }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        Aimweapon();
    }

    void FindClosestTarget()
    {
        TDEnemy[] enemies = FindObjectsOfType<TDEnemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (TDEnemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    void Aimweapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }

}
