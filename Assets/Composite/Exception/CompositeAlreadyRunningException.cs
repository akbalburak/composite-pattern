using UnityEngine.UIElements;

namespace Wave.Engine.Composite.Exception
{
    public class CompositeAlreadyRunningException : System.Exception
    {
        public CompositeObject CompositeObject { get; }
        public string ExceptionMessage { get; }
        public CompositeAlreadyRunningException(CompositeObject composite)
        {
            ExceptionMessage = $"{composite.name} - {composite.GetType()} was already running!";
            CompositeObject = composite;
        }

        public override string Message => $"{ExceptionMessage}";
    }
}
