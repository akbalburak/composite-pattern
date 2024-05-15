using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class ChangePosition : CompositeObject
    {
        public Transform Target;
        public bool LocalPosition;
        public Vector3 Position;
        
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            if (LocalPosition)
                Target.localPosition = Position;
            else
                Target.position = Position;

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Target == null)
                return base.GetCompositeName();

            return $"Change {Target.name} Position";
        }
    }
}
