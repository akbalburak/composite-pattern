using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wave.Engine.Composite.Actions.Physics;

namespace Wave.Engine.Composite.Editor
{
    public static class CompositePhysicsActionMenuItemEditor
    {
        [MenuItem("GameObject/Composite/Actions/Physics/Change Kinematic", false)]
        public static void CreateChangeKinematic()
        {
            GameObject obj = new GameObject("Change Kinematic", typeof(ChangeKinematic));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Physics/Change Velocity", false)]
        public static void CreateChangeVelocity()
        {
            GameObject obj = new GameObject("Change Velocity", typeof(ChangeVelocity));
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
