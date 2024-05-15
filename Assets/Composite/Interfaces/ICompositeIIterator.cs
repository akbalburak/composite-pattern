namespace Wave.Engine.Composite.Interfaces
{
    public interface ICompositeIIterator<T> where T : class
    {
        bool HasComposite();
        T NextComposite();
        T CurrentComposite();
        void ResetCompositeIterator();

        void AddComposite(CompositeObject composite);
        void RemoveComposite(CompositeObject composite);
    }
}
