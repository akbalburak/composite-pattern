using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Wave.Engine.Composite.Runner
{
    [DefaultExecutionOrder(1)]
    public class RunnerAtAwake : CompositeRunner
    {
        protected override void Awake()
        {
            base.Awake();
            StartRunning().Forget();
        }
    }
}
