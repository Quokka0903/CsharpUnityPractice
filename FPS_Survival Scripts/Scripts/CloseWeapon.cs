using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string CloseWeaponName;

    public bool isHand;
    public bool isAxe;
    public bool isPickAxe;
    
    public float range;
    public int damage;
    public float workSpeed;
    public float attackDelay;
    public float attackDelayActive;
    public float attackDelayDeactive;

    public Animator anim;

}
