using System;
using System.Collections.Generic;

namespace NArchitecture
{
    public class BusOptions
    {
        private IDictionary<Type, string> PolicyMap { get; } =
            new Dictionary<Type, string>();

        public EventComposition Events { get; }
        public RequestComposition Requests { get; }
        public AuthorizationComposition Authorization { get; }

        public BusOptions()
        {
            Events = new EventComposition();
            Requests = new RequestComposition();
            Authorization = new AuthorizationComposition();
        }

        public void AddPolicyFor<TMessage>(string policy)
            where TMessage : IMessage
        {
            Guard.AgainstEmptyString(nameof(policy), policy);

            AddPolicyFor(typeof(TMessage), policy);
        }

        public void AddPolicyFor(Type messageType, string policy)
        {
            Guard.AgainstNull(nameof(messageType), messageType);
            Guard.AgainstEmptyString(nameof(policy), policy);

            PolicyMap[messageType] = policy;
        }

        public string GetPolicyFor(Type messageType)
        {
            Guard.AgainstNull(nameof(messageType), messageType);

            if (!PolicyMap.ContainsKey(messageType))
            {
                return null;
            }

            return PolicyMap[messageType];
        }
    }
}
