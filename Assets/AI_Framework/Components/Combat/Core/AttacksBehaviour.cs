using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttacksBehaviour : MonoBehaviour
{
    protected EntityMediator enemy;

    protected virtual void Awake()
    {
        enemy = GetComponent<EntityMediator>();
    }

    public abstract void Execute();
}

