using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Conditions
{
    public class WaitComposite : CompositeObject
    {
        public CompositeObject Composite;
        public CompositeResult Result = CompositeResult.Succeed;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            await UniTask.WaitUntil(
                () => Composite.CurrentState == Result,
                cancellationToken: cancellationToken
            );

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Composite == null)
                return base.GetCompositeName();

            return $"Wait -{Composite.name}- Composite Until {Result}";
        }

    }
}
