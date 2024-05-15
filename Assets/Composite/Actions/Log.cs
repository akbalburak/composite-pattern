using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class Log : CompositeObject
    {
        public string LogMessage;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);
            Debug.Log(LogMessage);
            return SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            return $"LOG: {LogMessage}";
        }
    }
}
