using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class DestroyObject : CompositeObject
    {
        public GameObject Target;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            Destroy(Target);

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Target == null)
                return base.GetCompositeName();

            return $"Destroy {Target.name} Object";
        }
    }
}
