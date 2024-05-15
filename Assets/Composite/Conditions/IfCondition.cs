using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Conditions
{
    public abstract class IfCondition : CompositeObject
    {
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            if (Condition())
            {
                return base.SetState(CompositeResult.Succeed);
            }

            return base.SetState(CompositeResult.Failed);
        }

        protected abstract bool Condition();
    }
}
