using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemies/Movement Modifier ID")]
public class MovementModifierID : ScriptableObject
{
    [SerializeField] private string id;
    public string ID => id;
}
