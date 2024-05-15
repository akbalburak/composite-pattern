using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions.Physics
{
    public class ChangeKinematic : CompositeObject
    {
        public Rigidbody Rigidbody;
        public bool IsKinematic;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            Rigidbody.isKinematic = IsKinematic;

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Rigidbody == null)
                return base.GetCompositeName();

            return $"Change {Rigidbody.name} Rigidbody Kinematic State To {IsKinematic}";
        }
    }
}
