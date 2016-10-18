using System;

namespace NArchitecture
{
    public class MessagePolicy : IMessagePolicy
    {
        public MessagePolicy(Type messageType, string policyName)
        {
            MessageType = messageType;
            PolicyName = policyName;
        }

        public Type MessageType { get; }
        public string PolicyName { get; }
    }
}
