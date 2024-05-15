using Composite.Engine.Composite.Actions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wave.Engine.Composite.Actions;
using Wave.Engine.Composite.Samples;

namespace Wave.Engine.Composite.Editor
{
    public static class CompositeActionMenuItemEditor
    {
        [MenuItem("GameObject/Composite/Actions/Destroy Object", false)]
        public static void CreateDestroyObject()
        {
            GameObject obj = new GameObject("Destroy Object", typeof(DestroyObject));
            SetTransform(obj);
        }
        
        [MenuItem("GameObject/Composite/Actions/Pause Editor", false)]
        public static void CreatePauseEditor()
        {
            GameObject obj = new GameObject("Pause Editor", typeof(PauseEditor));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Wait For Seconds", false)]
        public static void CreateWaitForSeconds()
        {
            GameObject obj = new GameObject("Wait For Seconds", typeof(Actions.WaitForSeconds));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Activate Object", false)]
        public static void CreateActivateObject()
        {
            GameObject obj = new GameObject("Activate Object", typeof(ActivateObject));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Deactivate Object", false)]
        public static void CreateDeactivateObject()
        {
            GameObject obj = new GameObject("Deactivate Object", typeof(DeactivateObject));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Log", false)]
        public static void CreateLog()
        {
            GameObject obj = new GameObject("Log", typeof(Log));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Unity Event", false)]
        public static void CreateUnityEvent()
        {
            GameObject obj = new GameObject("Unity Event", typeof(ExecuteUnityEvent));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Change Rotation", false)]
        public static void CreateLocalRotate()
        {
            GameObject obj = new GameObject("Change Rotation", typeof(ChangeRotation));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Change Position", false)]
        public static void CreateLocalMove()
        {
            GameObject obj = new GameObject("Change Position", typeof(ChangePosition));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Play Animation", false)]
        public static void CreatePlayAnimation()
        {
            GameObject obj = new GameObject("Play Animation", typeof(PlayAnimation));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Play Cross Fade", false)]
        public static void CreatePlayCrossFade()
        {
            GameObject obj = new GameObject("Play Cross Fade", typeof(CrossFade));
            SetTransform(obj);
        }

       
        [MenuItem("GameObject/Composite/Actions/Terminate Runner", false)]
        public static void CreateTerminateRunner()
        {
            GameObject obj = new GameObject("Terminate Runner", typeof(TerminateRunner));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Start Runner", false)]
        public static void CreateStartRunner()
        {
            GameObject obj = new GameObject("Start Runner", typeof(StartRunner));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Play Sound", false)]
        public static void CreatePlaySound()
        {
            GameObject obj = new GameObject("Play Sound", typeof(PlaySound));
            SetTransform(obj);
        }

        [MenuItem("GameObject/Composite/Actions/Play Camera", false)]
        public static void CreatePlayCamera()
        {
            GameObject obj = new GameObject("Play Camera", typeof(PlayCamera));
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
