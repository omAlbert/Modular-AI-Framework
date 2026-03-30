using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy Logic/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    [SerializeField] private float maxHealth;

    [SerializeField] private EnemiesEnum enemyName;

    [Space(10f)]
    [Header("Knockback")]
    [Space(5f)]
    [SerializeField] private float knockBackTime;
    [Range(0f, 100f)][SerializeField] private float knockbackResistance;
    [Range(0f, 100f)][SerializeField] private float stunResistance;

    //[SerializeField] private bool custom;

    public float MaxHealth { get { return maxHealth; } private set { } }
    public EnemiesEnum EnemyName { get { return enemyName; } private set { } }
    public float KnockbackResistance { get { return knockbackResistance; } private set { } }
    public float KnockbackTime { get { return knockBackTime; } private set { } }
    public float StunResistance { get { return stunResistance; } private set { } }

/* #if UNITY_EDITOR

    [CustomEditor(typeof(EnemyStatsSO))]
    public class EnemyStatsSOEditor : Editor
    {
        EnemyStatsSO enemyStats;


        private void OnEnable()
        {
            enemyStats = (EnemyStatsSO)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (enemyStats.custom)
            {
                difficultTerrain.blockPlayerDashes = EditorGUILayout.Toggle("Block Player Dashes", difficultTerrain.blockPlayerDashes);
                difficultTerrain.blockPlayerJumpAir = EditorGUILayout.Toggle("Block Player Jump Air", difficultTerrain.blockPlayerJumpAir);
                difficultTerrain.blockPlayerHammerRebound = EditorGUILayout.Toggle("Block Player Hammer Rebound", difficultTerrain.blockPlayerHammerRebound);
            }

            if (difficultTerrain.affectEnemies)
            {
                EditorGUILayout.PropertyField(enemiesWhoAffectProperty, new GUIContent("Enemies Who Affect"), true);
            }

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(difficultTerrain);
            }
        }
    }

#endif*/
}

/*
public struct EnemyGrowthLevelData
{
    public float level;
    public float maxHealth;
    public int damageOnTouch;
}*/
