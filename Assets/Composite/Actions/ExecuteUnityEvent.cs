using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Samples
{
    public class ExecuteUnityEvent : CompositeObject
    {
        public UnityEvent UEventList;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            UEventList?.Invoke();

            return SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (UEventList == null)
                return base.GetCompositeName();
            
            int eventCount = UEventList.GetPersistentEventCount();
            string output = base.GetCompositeName();
            
            for (int i = 0; i < eventCount; i++)
                output += $" 1. {UEventList.GetPersistentTarget(i)?.name}/{UEventList.GetPersistentMethodName(i)}";

            return output;
        }
    }
}
