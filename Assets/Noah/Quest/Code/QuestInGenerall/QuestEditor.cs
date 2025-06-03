#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Quest;

[CustomEditor(typeof(QuestSystem))]
public class QuestEditor : Editor
{
    SerializedProperty questInfoProperty;
    SerializedProperty questRewardProperty;
    SerializedProperty questGoalListProperty;

    void OnEnable()
    {
        questInfoProperty = serializedObject.FindProperty("Information");
        questRewardProperty = serializedObject.FindProperty("Reward");
        questGoalListProperty = serializedObject.FindProperty("Goals");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Quest Info", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(questInfoProperty, true);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Reward", EditorStyles.boldLabel);
        if (questRewardProperty != null)
        {
            EditorGUILayout.PropertyField(questRewardProperty, true);
        }
        else
        {
            EditorGUILayout.HelpBox("Reward konnte nicht gefunden werden (null). Stelle sicher, dass es initialisiert ist.", MessageType.Warning);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Goals", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(questGoalListProperty, true);

        serializedObject.ApplyModifiedProperties();
    }

    [MenuItem("Assets/Create/Quest System/New Quest")]
    public static void CreateQuest()
    {
        var newQuest = ScriptableObject.CreateInstance<QuestSystem>();
        AssetDatabase.CreateAsset(newQuest, "Assets/NewQuest.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newQuest;
    }
}
#endif
