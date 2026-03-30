using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetModifier : MovementModifier
{
    [SerializeField] private Vector2 targetOffset;

    public override Vector2? ModifyTarget(EntityMediator enemy, Vector2 target)
    {
        return target + targetOffset;
    }
}
