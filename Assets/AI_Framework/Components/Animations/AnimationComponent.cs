using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    public AnimationMap animationMap;
    [SerializeField] private Animator animator;

    private AnimationID currentAnimationState;

    private bool doOverride;

    public bool DoOverride { get { return doOverride; } set { doOverride = value; } }
    public void ChangeAnimationState(AnimationID newAnimationStateID)
    {
        if (currentAnimationState == newAnimationStateID || doOverride)
            return;

        var stateName = animationMap.GetStateName(newAnimationStateID);
        animator.Play(stateName);

        currentAnimationState = newAnimationStateID;
    }
}

[System.Serializable]
public class AnimationBinding
{
    public AnimationID id;
    public string stateName;
}