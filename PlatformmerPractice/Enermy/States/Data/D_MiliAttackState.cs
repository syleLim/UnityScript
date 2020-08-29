using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMiliAttackStateData", menuName = "Data/State Data/MiliAttack Data")]
public class D_MiliAttackState : ScriptableObject
{
    public float attackRadius = 0.5f;
    public LayerMask whatIsPlayer;

    public float attackDamage = 10f;
}
