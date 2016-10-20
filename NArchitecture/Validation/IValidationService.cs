using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IValidationService
    {
        Task Validate(IServiceBus bus, IMessage message);
    }
}
