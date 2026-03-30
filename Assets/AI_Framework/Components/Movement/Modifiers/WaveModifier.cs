using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveModifier : MovementModifier
{
    [SerializeField] float amplitude = 1f;
    [SerializeField] float frequency = 2f;

    public override Vector2? ModifyTarget(EntityMediator enemy, Vector2 target)
    {
        float wave = Mathf.Sin(Time.time * frequency) * amplitude;
        return target + Vector2.up * wave;
    }
}
