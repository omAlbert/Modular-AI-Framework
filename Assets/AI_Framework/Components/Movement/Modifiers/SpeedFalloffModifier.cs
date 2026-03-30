using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedFalloffModifier : MovementModifier
{
    [Header("Mode")]
    [SerializeField] private FalloffMode falloffMode;

    [Header("Falloff Settings")]
    [SerializeField] private float slowDownDistance = 2f;
    [SerializeField] private float stopDistance = 0.1f;

    [Header("Velocity Settings")]
    [SerializeField] private float minVelocity = 0.1f;

    [SerializeField] private float falloffDuration = 0.15f;


    [Header("Optional Curve")]
    [SerializeField] private bool useCurve = false;
    [SerializeField] private AnimationCurve falloffCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public override float ModifySpeed(EntityMediator enemy, Vector2 target, float speed, float totalTime = 0f, float remainingTime = 0f)
    {
        float normalized = 1f;

        switch (falloffMode)
        {
            case FalloffMode.Distance:
                normalized = CalculateDistanceFalloff(enemy, target);
                break;

            case FalloffMode.Velocity:
                normalized = CalculateVelocityFalloff(enemy, speed);
                break;

            case FalloffMode.Time:
                normalized = CalculateTimedFalloff(totalTime, remainingTime);
                break;
        }

        if (useCurve)
            return falloffCurve.Evaluate(normalized);

        return normalized;
    }

    private float CalculateDistanceFalloff(EntityMediator enemy, Vector2 target)
    {
        float enemyX = enemy.transform.position.x;
        float distance = Mathf.Abs(target.x - enemyX);

        if (distance <= stopDistance)
            return 0f;

        if (distance >= slowDownDistance)
            return 1f;

        return Mathf.Clamp01(distance / slowDownDistance);
    }

    private float CalculateVelocityFalloff(EntityMediator enemy, float initialSpeed)
    {
        float currentVelocity = Mathf.Abs(enemy.E_Movement.EnemyRigidbody.velocity.x);

        if (currentVelocity <= minVelocity)
            return 0f;

        return Mathf.Clamp01(currentVelocity / initialSpeed);
    }

    private float CalculateTimedFalloff(float totalTime, float remainingTime)
    {
        float elapsed = totalTime - remainingTime;

        if (elapsed < totalTime - falloffDuration)
            return 0f; 

        float normalized = (elapsed - (totalTime - falloffDuration)) / falloffDuration;

        return Mathf.Clamp01(normalized);
    }
}

public enum FalloffMode
{
    Distance,
    Velocity,
    Time
}
