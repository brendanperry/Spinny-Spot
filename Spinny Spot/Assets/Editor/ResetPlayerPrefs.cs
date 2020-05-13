using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ResetPrefs))]
[CanEditMultipleObjects]
public class ResetPlayerPrefs : Editor {
   
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        ResetPrefs script = (ResetPrefs)target;
        if (GUILayout.Button("Reset All PlayerPrefs Data")) {
            script.DeletePrefs();
        }
    }
}
