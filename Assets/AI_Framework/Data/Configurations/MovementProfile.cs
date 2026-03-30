using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/Movements Mods/Movement Profile")]
public class MovementProfile : ScriptableObject
{
    [SerializeField] private List<MovementModifierID> modifiers;
    public List<MovementModifierID> Modifiers => modifiers;
}
