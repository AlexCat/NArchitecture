using NArchitecture.Security;

namespace NArchitecture.Tests.Security
{
    [Authorize("Over21")]
    public class PurchaseAlcohol : IMessage
    {
    }
}
