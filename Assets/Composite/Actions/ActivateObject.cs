using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Samples
{
    public class ActivateObject : CompositeObject
    {
        public GameObject Target;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            Target.SetActive(true);

            return SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Target == null)
                return base.GetCompositeName();

            return $"Activate {Target.name} Object";
        }
    }
}
