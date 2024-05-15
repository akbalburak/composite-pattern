using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class WaitForSeconds : CompositeObject
    {
        public float WaitSeconds;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            await UniTask.WaitForSeconds(WaitSeconds, cancellationToken: cancellationToken);

            return SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            return $"Wait For {WaitSeconds}s seconds";
        }
    }
}
