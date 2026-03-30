using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsComponent : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO baseStats;
    public EnemyStatsSO BaseStats => baseStats;

    //public EnemyStatsSO runtimeStats;

    void Awake()
    {
        //runtimeStats = Instantiate(baseStats);
    }
}
