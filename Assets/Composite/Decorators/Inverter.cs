/*
 * Invert the child response; 
 * If the child composite response is success return failure.
 * If the composite child response is failure return success.
 */
using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite;
using Wave.Engine.Composite.Enums;

namespace Composite.Engine.Composite.Decorators
{
    public class Inverter : CompositeOutputObject
    {
        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            CompositeResult result = await base._composite.Run(cancellationToken);

            if (result == CompositeResult.Succeed)
                return base.SetState(CompositeResult.Failed);

            if (result == CompositeResult.Failed)
                return base.SetState(CompositeResult.Succeed);

            return base.SetState(result);
        }

        protected override string GetCompositeName()
        {
            return $"[INVERTER]";
        }
    }
}
