using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;

[InitializeOnLoad]
public class HierarchyToggleDrawer
{

    static HierarchyToggleDrawer()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
    }

    static readonly int IconSize = 16;

    static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
    {
        var objectContent = EditorGUIUtility.ObjectContent(EditorUtility.InstanceIDToObject(instanceID), null);
        Object obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj == null)
            return;
        GameObject gameObject = (GameObject)obj;
        Rect posRect = new Rect(selectionRect.xMax - IconSize, selectionRect.yMin, IconSize, IconSize);
        bool newBool = GUI.Toggle(posRect, gameObject.activeSelf, "");

        if (newBool != gameObject.activeSelf) {
            gameObject.SetActive(newBool);
            if (Application.isPlaying)
                return;
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null) {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            } else {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }

        }
    }
}
