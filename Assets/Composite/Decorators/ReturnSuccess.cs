/*
 * Return always success even if child composite returns failure.
 */
using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Decorators
{
    public class ReturnSuccess : CompositeOutputObject
    {
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            await _composite.Run(cancellationToken);

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            return $"[ALWAYS SUCCESS]";
        }
    }
}
