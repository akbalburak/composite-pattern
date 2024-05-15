using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Conditions
{
    public abstract class WaitUntilCondition : CompositeObject
    {
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            await UniTask.WaitUntil(Condition, cancellationToken: cancellationToken);

            return base.SetState(CompositeResult.Succeed);
        }

        protected abstract bool Condition();
    }
}
