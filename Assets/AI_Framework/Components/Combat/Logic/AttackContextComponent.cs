using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContextComponent : MonoBehaviour
{
    public event Action OnAttackPrepared;

    private List<EnemyAttackData> attacksOnCooldown;
    public List<EnemyAttackData> AttacksOnCooldown => attacksOnCooldown;

    public CurrentAttackContext CurrentAttackContext {  get; private set; }

    public bool IsEnemyOnrecovery { get; private set; }

    public event Action OnFinished;

    private void Awake()
    {
        attacksOnCooldown = new();
        CurrentAttackContext = new();
        CurrentAttackContext.ClearAllData();
    }
    public void ClearAttack()
    {
        CurrentAttackContext.ClearAllData();
    }

    public void StartPrepareAttack(EnemyAttackData data)
    {
        if (data is not IPrepareAttack preparable)
        {
            OnAttackPrepared?.Invoke();
            return;
        }

        TimersManager.Instance.StartRuntimeTimer(
            preparable.PrepareTime,
            () => OnAttackPrepared?.Invoke()
        );
    }

    public void SetCurrentAttack(EnemyAttackData attack)
    {
        CurrentAttackContext.AttackData = attack;
    }

    public void SetAttackInCooldown(EnemyAttackData attack)
    {
        attacksOnCooldown.Add(attack);
        TimersManager.Instance.StartRuntimeTimer(attack.Cooldown, () =>
        {
            attacksOnCooldown.Remove(attack);
        });
    }

    public void SetEnemyOnRecovery(EnemyAttackData data)
    {
        IsEnemyOnrecovery = true;
        TimersManager.Instance.StartRuntimeTimer(data.Recovery, () =>
        {
            IsEnemyOnrecovery = false;
        });
    }

    public void AttackEnded(EnemyAttackData data = null)
    {
        if (data == null)
            data = CurrentAttackContext.AttackData;

        OnFinished?.Invoke();
        SetAttackInCooldown(data);
        ClearAttack();
    }
}

[System.Serializable]
public class CurrentAttackContext
{
    public EnemyAttackData AttackData { get; set; }
    public float TotalDuration { get; set; }
    public float RemainingTime { get; set; }
    public void ClearAllData()
    {
        AttackData = null;
        TotalDuration = 0;
        RemainingTime = 0;
    }
}
