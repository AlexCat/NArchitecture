using System;

namespace NArchitecture
{
    public class MessageAuthorization : IMessageAuthorization
    {
        public MessageAuthorization(Type messageType, string policy, string roles)
        {
            MessageType = messageType;
            Policy = policy;
            Roles = roles;
        }

        public Type MessageType { get; }
        public string Policy { get; }
        public string Roles { get; }
    }
}
