/*
 * Return always failure even if child composite returns success.
 */
using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Decorators
{
    public class ReturnFailure : CompositeOutputObject
    {
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);
            
            await _composite.Run(cancellationToken);

            return base.SetState(CompositeResult.Failed);
        }

        protected override string GetCompositeName()
        {
            return $"[ALWAYS SUCCESS]";
        }
    }
}
