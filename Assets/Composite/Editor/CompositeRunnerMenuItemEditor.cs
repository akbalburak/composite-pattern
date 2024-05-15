using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wave.Engine.Composite.Runner;
using Wave.Engine.Composite.Runners;

namespace Wave.Engine.Composite.Editor
{
    public static class CompositeRunnerMenuItemEditor
    {
        [MenuItem("GameObject/Composite/Runner/Runner At Start", false)]
        public static void CreateRunnerAtStart()
        {
            GameObject selector = new("Runner At Start", typeof(RunnerAtStart));
            SetTransform(selector);
        }

        [MenuItem("GameObject/Composite/Runner/Runner At Awake", false)]
        public static void CreateRunnerAtAwake()
        {
            GameObject selector = new("Runner At Awake", typeof(RunnerAtAwake));
            SetTransform(selector);
        }

        [MenuItem("GameObject/Composite/Runner/Manual Runner", false)]
        public static void CreateManualRunner()
        {
            GameObject selector = new("Manual Runner", typeof(RunnerManual));
            SetTransform(selector);
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
