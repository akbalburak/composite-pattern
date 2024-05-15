using UnityEngine;
using Wave.Engine.Composite.Attributes;

namespace Wave.Engine.Composite
{
    public abstract class CompositeOutputObject : CompositeObject
    {
        [SerializeField, ReadOnly]
        protected CompositeObject _composite;

        protected virtual void Awake()
        {
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                    continue;

                if (!child.TryGetComponent(out _composite))
                    continue;

                break;
            }
        }
    }
}
