using Cysharp.Threading.Tasks;
using System.Threading;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class StartRunner : CompositeObject
    {
        public CompositeRunner Runner;
        public bool WaitRunnerComplete;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            if (WaitRunnerComplete)
                await Runner.StartRunning();
            else
                Runner.StartRunning().Forget();

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Runner == null)
                return base.GetCompositeName();

            if (!WaitRunnerComplete)
                return $"Start {Runner.name} Runner";

            return $"Start {Runner.name} Runner and Wait For Complete";
        }
    }
}
