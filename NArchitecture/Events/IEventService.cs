using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IEventService
    {
        Task Notify<TEvent>(IBus bus, TEvent @event)
            where TEvent : IEvent;
    }
}
