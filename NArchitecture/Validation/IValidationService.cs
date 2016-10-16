using System.Threading.Tasks;

namespace NArchitecture.Validation
{
    public interface IValidationService
    {
        Task Validate(IMessage message);
    }
}
