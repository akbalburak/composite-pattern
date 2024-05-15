using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class ChangeRotation : CompositeObject
    {
        public Transform Target;
        public bool LocalRotation;
        public Vector3 Rotation;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            if (LocalRotation)
                Target.localEulerAngles = Rotation;
            else
                Target.eulerAngles = Rotation;

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Target == null)
                return base.GetCompositeName();

            return $"Change {Target.name} Rotation";
        }
    }
}
