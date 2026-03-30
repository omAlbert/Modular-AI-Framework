using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksSelectorComponent : MonoBehaviour
{
    [SerializeField] private AttackRequirementsComponent requirements;

    private readonly List<EnemyAttackData> validAttacks = new();

    private EntityMediator enemy;

    private void Awake()
    {
        enemy = GetComponent<EntityMediator>();
    }

    public EnemyAttackData SelectAttack(IReadOnlyList<EnemyAttackData> attacks)
    {
        validAttacks.Clear();

        foreach (var attack in attacks)
        {
            if (attack == null)
                continue;

            if (!enemy.E_AttacksattacksRequirments.CanUseAttack(attack))
                continue;

            validAttacks.Add(attack);
        }

        if (validAttacks.Count == 0)
            return null;

        return ChooseWeighted(validAttacks);
    }

    private EnemyAttackData ChooseWeighted(List<EnemyAttackData> attacks)
    {
        int totalWeight = 0;

        foreach (var attack in attacks)
            totalWeight += Mathf.Max(1, attack.Priority);

        int roll = Random.Range(0, totalWeight);

        foreach (var attack in attacks)
        {
            roll -= Mathf.Max(1, attack.Priority);
            if (roll < 0)
                return attack;
        }

        return attacks[0];
    }

    public bool HasAvailableAttack(IReadOnlyList<EnemyAttackData> attacks)
    {
        foreach (var attack in attacks)
        {
            if (requirements.CanUseAttack(attack))
                return true;
        }
        return false;
    }
}
