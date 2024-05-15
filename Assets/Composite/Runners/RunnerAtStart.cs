using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Wave.Engine.Composite.Runner
{
    [DefaultExecutionOrder(1)]
    public class RunnerAtStart : CompositeRunner
    {
        private void Start()
        {
            StartRunning().Forget();
        }
    }
}
