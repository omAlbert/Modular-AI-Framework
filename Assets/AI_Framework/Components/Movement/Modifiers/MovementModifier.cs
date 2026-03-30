using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementModifier : MonoBehaviour
{
    [SerializeField] private MovementModifierID modifierID;
    public MovementModifierID ID => modifierID;

    public virtual Vector2? ModifyTarget(EntityMediator enemy, Vector2 target) => null;
   
    public virtual float ModifySpeed(EntityMediator enemy, Vector2 target, float speed, float totalTime = 0f, float remainingTime = 0f) => 1f;
}
