using System;

namespace NArchitecture
{
    public interface IMessagePolicy
    {
        Type MessageType { get; }
        string PolicyName { get; }
    }
}
