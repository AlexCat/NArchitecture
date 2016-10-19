using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IEventHandler
    {
        Task Handle(EventHandlerContext context);
    }
}
