using UnityEngine;
using Wave.Engine.Composite.Runner;

namespace Wave.Engine.Composite.Tasks
{
    public abstract class CompositeTerminate : MonoBehaviour
    {
        [SerializeField]
        protected RunnerAtStart _stopRunnerOnCancel;

        [SerializeField]
        protected RunnerAtStart _startRunnerOnCancel;
    }
}