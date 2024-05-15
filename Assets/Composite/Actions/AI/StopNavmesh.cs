using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.AI;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions.AI
{
    public class StopNavmesh : CompositeObject
    {
        public NavMeshAgent NavmeshAgent;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            NavmeshAgent.isStopped = true;

            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (NavmeshAgent == null)
                return base.GetCompositeName();

            return $"Stop {NavmeshAgent.name} Navmesh Agent";
        }
    }
}
