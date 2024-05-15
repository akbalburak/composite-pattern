using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wave.Engine.Composite.Conditions;

namespace Wave.Engine.Composite.Editor
{
    public static class ConditionsMenuItemEditor
    {
        [MenuItem("GameObject/Composite/Conditions/Wait Composite", false)]
        public static void CreateDestroyObject()
        {
            GameObject obj = new GameObject("Wait Composite Object", typeof(WaitComposite));
            SetTransform(obj);
        }

        private static void SetTransform(GameObject obj)
        {
            obj.transform.parent = Selection.activeTransform;
            Selection.activeGameObject = obj;

            EditorSceneManager.MarkSceneDirty(
               EditorSceneManager.GetActiveScene()
            );
        }
    }
}
