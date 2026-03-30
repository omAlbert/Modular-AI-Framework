using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackLoadout : MonoBehaviour
{
    [SerializeField]
    private List<EnemyAttackData> availableAttacks;

    public IReadOnlyList<EnemyAttackData> Attacks => availableAttacks;
}
