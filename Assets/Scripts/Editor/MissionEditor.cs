#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Mission))]
public class MissionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var mission = (Mission) target;
        mission.title = EditorGUILayout.TextField("Title", mission.title);
        mission.textUI_En = EditorGUILayout.TextField("Text in UI in English", mission.textUI_En);
        mission.textUI_Pt = EditorGUILayout.TextField("Text in UI in Portuguese", mission.textUI_Pt);
        mission.missionType = (MissionType) EditorGUILayout.EnumPopup("Mission Type", mission.missionType);
        switch (mission.missionType)
        {
            case MissionType.Collect:
                PrepareNewLabel("resourceType", "Resource Type");
                PrepareNewLabel("amountOfResources", "Amount Of Resources");
                break;
            case MissionType.Destroy:
                PrepareNewLabel("destroyType", "Destroy Type");
                switch (mission.destroyType)
                {
                    case DestroyType.Crystals:
                        PrepareNewLabel("amountToDestroyed", "Amount To Be Destroyed");
                        break;
                }

                break;
        }
    }

    private void PrepareNewLabel(string propertyName, string displayName)
    {
        var newLabel = serializedObject.FindProperty(propertyName);
        EditorGUILayout.PropertyField(newLabel, new GUIContent(displayName), true);
        newLabel.serializedObject.ApplyModifiedProperties();
    }
}
#endif