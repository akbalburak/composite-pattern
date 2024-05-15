using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class PauseEditor : CompositeObject
    {
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPaused = true;
            UnityEditor.Selection.activeGameObject = gameObject;
#endif

            return base.SetState(CompositeResult.Succeed);
        }
    }
}
