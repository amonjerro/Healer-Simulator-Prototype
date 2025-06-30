namespace Prototype
{
    public enum ServiceMessageTypes
    {
        ActorRegistered,
        PlayerRegistered
    }

    public abstract class ServiceMessage
    {
        public ServiceMessageTypes Type { get; protected set; }
        public abstract object MessageValue { get; }
    }
    public class ServiceMessage<T> : ServiceMessage
    {
        public override object MessageValue
        {
            get { return Message; }
        }

        public T Message { get; private set; }


        public ServiceMessage(T content, ServiceMessageTypes type){
            Message = content;
            Type = type;
        }
    }
}
