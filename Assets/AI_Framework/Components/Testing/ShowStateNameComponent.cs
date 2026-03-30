using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowStateNameComponent : MonoBehaviour
{
    private EntityMediator enemy;

    [SerializeField] private TMP_Text text;

    private string currentStateName = "";

    private void Awake()
    {
        enemy = GetComponent<EntityMediator>();
    }

    private void Update()
    {
        if (enemy.FSM.GetCurrentEnemyStateName != currentStateName)
        {
            currentStateName = enemy.FSM.GetCurrentEnemyStateName.Replace("_", " ");
            text.text = currentStateName;
        }
    }
}
