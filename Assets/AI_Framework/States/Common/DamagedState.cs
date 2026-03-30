using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Logic/Common/Damaged")]
public class DamagedState : EnemyStateSOBase
{
    [SerializeField] private AnimationID animationName;

    public override void DoEnterLogic()
    {
        enemy.E_Animations.ChangeAnimationState(animationName);
    }
}
