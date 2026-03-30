using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/Animation_Map")]

public class AnimationMap : ScriptableObject
{
    [SerializeField] private List<AnimationBinding> bindings;
    public List<AnimationBinding> Bindings => bindings;

    public string GetStateName(AnimationID id)
    {
        var binding = bindings.Find(b => b.id == id);
        if (binding == null)
        {
            Debug.LogError($"No binding for {id.name}");
            return null;
        }
        return binding.stateName;
    }
}
