using NArchitecture.Security;
using System;

namespace NArchitecture
{
    public interface IMessageAuthorization : IAuthorizeData
    {
        Type MessageType { get; }
    }
}
