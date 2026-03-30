#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyStates))]
public class StateInlineDrawer : PropertyDrawer
{
    private static readonly string FoldoutKeyPrefix = "EnemyStatesFoldout_";

    private bool IsFoldoutOpen(SerializedProperty property)
    {
        string key = FoldoutKeyPrefix + property.propertyPath;
        if (!EditorPrefs.HasKey(key))
        {
            EditorPrefs.SetBool(key, true);
        }
        return EditorPrefs.GetBool(key);
    }

    private void SetFoldoutState(SerializedProperty property, bool state)
    {
        string key = FoldoutKeyPrefix + property.propertyPath;
        EditorPrefs.SetBool(key, state);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        SerializedProperty stateIDProp = property.FindPropertyRelative("stateID");
        SerializedProperty initialStateProp = property.FindPropertyRelative("initialState");
        SerializedProperty enemyStateProp = property.FindPropertyRelative("enemyState");

        Rect foldoutRect = new Rect(position.x, position.y, position.width, lineHeight);
        bool isOpen = IsFoldoutOpen(property);

        string foldoutName = "State";

        if (stateIDProp != null && stateIDProp.objectReferenceValue != null)
        {
            foldoutName = stateIDProp.objectReferenceValue.name;
        }

        isOpen = EditorGUI.Foldout(foldoutRect, isOpen, foldoutName, true);
        SetFoldoutState(property, isOpen);

        if (isOpen)
        {
            position.y += lineHeight + spacing;

            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, lineHeight), stateIDProp);
            position.y += lineHeight + spacing;

            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, lineHeight), initialStateProp);
            position.y += lineHeight + spacing;

            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, lineHeight), enemyStateProp);
            position.y += lineHeight + spacing;

            if (enemyStateProp.objectReferenceValue != null)
            {
                SerializedObject so = new SerializedObject(enemyStateProp.objectReferenceValue);
                var properties = GetVisibleProperties(so);

                foreach (var prop in properties)
                {
                    EditorGUI.PropertyField(new Rect(position.x + 10, position.y, position.width - 10, lineHeight), prop, true);
                    position.y += EditorGUI.GetPropertyHeight(prop, true) + spacing;
                }

                so.ApplyModifiedProperties();
            }
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        if (IsFoldoutOpen(property))
        {
            height += 3 * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);

            SerializedProperty enemyStateProp = property.FindPropertyRelative("enemyState");
            if (enemyStateProp.objectReferenceValue != null)
            {
                SerializedObject so = new SerializedObject(enemyStateProp.objectReferenceValue);
                var properties = GetVisibleProperties(so);

                foreach (var prop in properties)
                {
                    height += EditorGUI.GetPropertyHeight(prop, true) + EditorGUIUtility.standardVerticalSpacing;
                }
            }
        }
        return height;
    }

    private List<SerializedProperty> GetVisibleProperties(SerializedObject serializedObject)
    {
        List<SerializedProperty> properties = new List<SerializedProperty>();
        SerializedProperty prop = serializedObject.GetIterator();
        if (prop.NextVisible(true))
        {
            while (prop.NextVisible(false))
            {
                properties.Add(prop.Copy());
            }
        }
        return properties;
    }
}
#endif
