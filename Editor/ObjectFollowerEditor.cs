using UnityEngine;
using UnityEditor;
using Ineor.Utils.ObjectFollower;

/// <summary>
/// This class defines a new more simple inspector for the ObjectFollow.cs script.
///
/// Made by: Mihael Golob, 30. 8. 2022
/// </summary>
[CustomEditor(typeof(ObjectFollower))]
public class ObjectFollowerEditor : Editor {
    public override void OnInspectorGUI() {
        var script = target as ObjectFollower;

        // OBJECT TO FOLLOW
        EditorGUILayout.LabelField("The object to follow", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_target"));
        
        // FOLLOW POSITION
        var followPosition = serializedObject.FindProperty("_followPosition");
        EditorGUILayout.PropertyField(followPosition);

        if (followPosition.boolValue) {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_positionOffset"));

            var lerp = serializedObject.FindProperty("_lerpMovement");
            EditorGUILayout.PropertyField(lerp);
            if (lerp.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_lerpMovementSpeed"),
                    new GUIContent("Lerp speed", "smooth movement"));
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        
        // FOLLOW ROTATION
        var followRotation = serializedObject.FindProperty("_followRotationMode");
        EditorGUILayout.PropertyField(followRotation);
        
        if (followRotation.intValue == 2) {
            EditorGUI.indentLevel++;
            var lerp = serializedObject.FindProperty("_lerpRotation");
            EditorGUILayout.PropertyField(lerp);
            if (lerp.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_lerpRotationSpeed"),
                    new GUIContent("Lerp speed", "smooth rotation"));
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        
        // FOLLOW SCALE
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_followScale"));

        //update the serialized fields
        serializedObject.ApplyModifiedProperties();
    }
}