using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wave.Engine.Composite.TweenActions;

namespace Wave.Engine.Composite.Editor
{
    public static class CompositeTweenActionMenuItemEditor
    {
        [MenuItem("GameObject/Composite/Tween Actions/Punch Scale", false)]
        public static void CreatePunchScale()
        {
            GameObject obj = new GameObject("Punch Scale", typeof(TPunchScale));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Tween Actions/Local Rotate", false)]
        public static void CreateLocalRotate()
        {
            GameObject obj = new GameObject("Rotate Local", typeof(TRotate));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Tween Actions/Local Move", false)]
        public static void CreateLocalMove()
        {
            GameObject obj = new GameObject("Rotate Move", typeof(TMove));
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
