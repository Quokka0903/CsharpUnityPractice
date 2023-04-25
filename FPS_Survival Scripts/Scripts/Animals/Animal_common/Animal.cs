using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField]
    protected string animalName;
    [SerializeField]
    protected int animalHp;
    protected int currentHp;
    [SerializeField]
    protected float walkSpeed;
    [SerializeField]
    protected float runSpeed;

    protected Vector3 destination;
    [SerializeField]
    protected float destinationRange;

    protected bool isAction;
    protected bool isWalking;
    protected bool isRunning;
    protected bool isChasing;
    protected bool isAttacking;
    protected bool isDead;


    [SerializeField]
    protected float walkTime;
    [SerializeField]
    protected float RunTime;
    [SerializeField]
    protected float waitTime;
    protected float currentTime;

    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected CapsuleCollider capCol;
    protected AudioSource theAudio;
    protected NavMeshAgent nav;
    protected FieldViewAngle theViewAngle;

    [SerializeField]
    protected AudioClip[] sound_animal_normal;
    [SerializeField]
    protected AudioClip sound_animal_hurt;
    [SerializeField]
    protected AudioClip sound_animal_dead;


    void Start()
    {
        theViewAngle = GetComponent<FieldViewAngle>();
        nav = GetComponent<NavMeshAgent>();
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        currentHp = animalHp;
        isAction = true;
    }
    protected virtual void Update()
    {
        if (!isDead)
        {
            Move();
            //Rotation();
            ElapseTime();
        }

    }
    protected void Move()
    {
        if (isWalking || isRunning)
        {
            //rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
            nav.SetDestination(transform.position + destination * 5f);
        }
    }

    //protected void Rotation()
    //{
    //    if (isWalking || isRunning)
    //    {
    //        Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, destination.y, 0f), tunningSpeed);
    //        rigid.MoveRotation(Quaternion.Euler(_rotation));
    //    }
    //}

    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && !isChasing && !isAttacking)
            {
                ResetAction();
            }
        }
    }

    protected virtual void ResetAction()
    {
        nav.ResetPath();

        isWalking = false;
        isRunning = false;
        isAction = true;

        nav.speed = walkSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-destinationRange, destinationRange), 0f, Random.Range(-destinationRange * 2f, destinationRange * 5f));
    }
       

    protected void Wait()
    {
        currentTime = waitTime;
    }

    protected void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
    }
    protected void Peek()
    {
        anim.SetTrigger("Peek");
        currentTime = waitTime;
    }
    protected void Sit()
    {
        anim.SetTrigger("Sit");
        currentTime = walkTime;
    }
    protected void TryWalk()
    {
        isWalking = true;
        currentTime = walkTime;
        nav.speed = walkSpeed;
        anim.SetBool("Walking", isWalking);
    }


    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {

            currentHp -= _dmg;
            Debug.Log(currentHp);
            if (currentHp <= 0)
            {
                Dead();
                return;
            }

            //PlaySE(sound_animal_hurt);
            anim.SetTrigger("Hurt");
        }
    }

    protected void Dead()
    {
        //PlaySE(sound_animal_dead);
        isDead = true;
        isRunning = false;
        isWalking = false;
        isChasing = false;
        isAttacking = false;
        StopAllCoroutines();
        anim.SetTrigger("Dead");
    }

    protected void RandomSound()
    {
        int _random = Random.Range(0, 3);
        PlaySE(sound_animal_normal[_random]);
    }

    protected void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
