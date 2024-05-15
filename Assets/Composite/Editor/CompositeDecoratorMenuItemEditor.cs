using Composite.Engine.Composite.Decorators;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wave.Engine.Composite.Decorators;

namespace Wave.Engine.Composite.Editor
{
    public static class CompositeDecoratorMenuItemEditor
    {
        [MenuItem("GameObject/Composite/Decorators/Return Success", false)]
        public static void CreateReturnSuccess()
        {
            GameObject obj = new GameObject("Return Success", typeof(ReturnSuccess));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Decorators/Return Failure", false)]
        public static void CreateReturnFailure()
        {
            GameObject obj = new GameObject("Return Failure", typeof(ReturnFailure));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Decorators/Repeater", false)]
        public static void CreateRepeaterTask()
        {
            GameObject obj = new GameObject("Repeater", typeof(Repeater));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Decorators/Inverter", false)]
        public static void CreateInverterTask()
        {
            GameObject obj = new GameObject("Inverter", typeof(Inverter));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Decorators/Only Once", false)]
        public static void CreateOnlyOnceTask()
        {
            GameObject obj = new GameObject("Only Once", typeof(OnlyOnce));
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
