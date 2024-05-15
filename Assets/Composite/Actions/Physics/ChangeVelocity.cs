using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions.Physics
{
    public class ChangeVelocity : CompositeObject
    {
        public Rigidbody RigidBody;
        public Vector3 Velocity;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            RigidBody.velocity = Velocity;

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (RigidBody == null)
                return base.GetCompositeName();

            return $"Change {RigidBody.name} Rigidbody Velocity To {Velocity}";
        }
    }
}
