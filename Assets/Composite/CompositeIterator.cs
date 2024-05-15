using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wave.Engine.Composite.Attributes;
using Wave.Engine.Composite.Interfaces;

namespace Wave.Engine.Composite
{
    public abstract class CompositeIterator : CompositeObject, ICompositeIIterator<CompositeObject>
    {
        [SerializeField, ReadOnly]
        private List<CompositeObject> _compositeCollection = new List<CompositeObject>();

        private int _currentIndex;

        public virtual void Awake()
        {
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                    continue;

                if (!child.TryGetComponent(out CompositeObject composite))
                    continue;

                _compositeCollection.Add(composite);
            }
        }

        public void AddComposite(CompositeObject composite)
        {
            _compositeCollection.Add(composite);
        }
        public void RemoveComposite(CompositeObject composite)
        {
            int compositeIndex = _compositeCollection.IndexOf(composite);
            if (compositeIndex == -1)
                return;

            if (_currentIndex > compositeIndex)
                _currentIndex--;

            _compositeCollection.RemoveAt(compositeIndex);
        }

        protected void LoadCompositeCollection(IList<CompositeObject> newCollection)
        {
            _compositeCollection.Clear();
            _compositeCollection.AddRange(newCollection);
        }
        protected IList<CompositeObject> GetCompositeCollection()
        {
            return _compositeCollection.ToList();
        }

        public CompositeObject CurrentComposite()
        {
            return _compositeCollection[_currentIndex];
        }
        public CompositeObject NextComposite()
        {
            if (!HasComposite())
                throw new ArgumentOutOfRangeException(nameof(_compositeCollection));

            return _compositeCollection[_currentIndex++];
        }

        public bool HasComposite()
        {
            return _currentIndex < _compositeCollection.Count;
        }

        public void ResetCompositeIterator()
        {
            _currentIndex = 0;
        }

        protected override string GetCompositeName()
        {
            return $"[{base.GetCompositeName()}]";
        }
    }
}
