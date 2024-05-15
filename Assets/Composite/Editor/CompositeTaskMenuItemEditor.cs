using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wave.Engine.Composite.Tasks;

namespace Wave.Engine.Composite.Editor
{
    public static class CompositeTaskMenuItemEditor
    {
        [MenuItem("GameObject/Composite/Tasks/Sequence", false)]
        public static void CreateSequenceTask()
        {
            GameObject sequence = new GameObject("Sequence", typeof(Sequence));
            SetTransform(sequence);
        }

        [MenuItem("GameObject/Composite/Tasks/Parallel", false)]
        public static void CreateParallelTask()
        {
            GameObject parallel = new GameObject("Parallel", typeof(Parallel));
            SetTransform(parallel);
        }

        [MenuItem("GameObject/Composite/Tasks/Parallel Selector", false)]
        public static void CreateParallelSelectorTask()
        {
            GameObject parallelSelector = new GameObject("Parallel Selector", typeof(ParallelSelector));
            SetTransform(parallelSelector);
        }

        [MenuItem("GameObject/Composite/Tasks/Sequence Selector", false)]
        public static void CreateSelectorTask()
        {
            GameObject selector = new("Sequence Selector", typeof(SequenceSelector));
            SetTransform(selector);
        }

        [MenuItem("GameObject/Composite/Tasks/Random Sequence", false)]
        public static void CreateRandomSequenceTask()
        {
            GameObject selector = new("Random Sequence", typeof(RandomSequence));
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
