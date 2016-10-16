using NArchitecture.Security;

namespace NArchitecture.Tests.Security
{
    [Authorize("Over21")]
    public class DrinkAlcohol : IMessage
    {
    }
}
