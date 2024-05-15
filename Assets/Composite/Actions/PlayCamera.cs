using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Wave.Engine.Composite.Enums;

namespace Wave.Engine.Composite.Actions
{
    public class PlayCamera : CompositeObject
    {
        public CinemachineVirtualCameraBase Camera;
        public Transform Target;
        public float Delay;
        public float Duration;

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            await base.Run(cancellationToken);

            if (Camera != null)
            {
                if (Delay > 0)
                    await UniTask.WaitForSeconds(Delay, cancellationToken: cancellationToken);

                bool wasActive = Camera.gameObject.activeSelf;
                Transform oldFollow = Camera.Follow;
                Transform oldLookAt = Camera.LookAt;

                Camera.Follow = Target;
                Camera.LookAt = Target;

                Camera.gameObject.SetActive(true);

                if (Duration > 0)
                    await UniTask.WaitForSeconds(Duration, cancellationToken: cancellationToken);

                if (Camera.Follow == Target)
                    Camera.Follow = oldFollow;

                if (Camera.LookAt == Target)
                    Camera.LookAt = oldLookAt;

                Camera.gameObject.SetActive(wasActive);

            }


            return base.SetState(CompositeResult.Succeed);
        }

        protected override string GetCompositeName()
        {
            if (Camera == null)
                return base.GetCompositeName();

            if (Target == null)
                return $"Play {Camera.Name} Camera For {Duration}s With {Delay}s Delay";

            return $"Look At {Target.name} With {Camera.Name} Camera For {Duration}s After {Delay}s Delay";
        }
    }
}
