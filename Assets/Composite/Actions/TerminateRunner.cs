using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite;
using Wave.Engine.Composite.Enums;

namespace Composite.Engine.Composite.Actions
{
    public class TerminateRunner : CompositeObject
    {
        public CompositeRunner Runner;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            Runner.StopRunning();

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Runner == null)
                return base.GetCompositeName();

            return $"Terminate {Runner.name} Runner";
        }
    }
}
