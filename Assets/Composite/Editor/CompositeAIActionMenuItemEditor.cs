using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wave.Engine.Composite.Actions.AI;

namespace Wave.Engine.Composite.Editor
{
    public static class CompositeAIActionMenuItemEditor
    {
        [MenuItem("GameObject/Composite/Actions/AI/Stop Navmesh", false)]
        public static void CreateStopNavmesh()
        {
            GameObject obj = new GameObject("Stop Navmesh Object", typeof(StopNavmesh));
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
