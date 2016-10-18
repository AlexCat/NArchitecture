using NArchitecture.Events;
using NArchitecture.Requests;
using NArchitecture.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NArchitecture
{
    public class BusOptions
    {
        public EventComposition Events { get; }
        public RequestComposition Requests { get; }
        public AuthorizationComposition Authorization { get; }
        public IList<IMessagePolicy> MessagePolicies { get; }

        public BusOptions()
        {
            Events = new EventComposition();
            Requests = new RequestComposition();
            Authorization = new AuthorizationComposition();
            MessagePolicies = new List<IMessagePolicy>();
        }

        public void AddMessagePolicy<TMessage>(params string[] policyNames)
            where TMessage : IMessage
        {
            foreach(var policyName in policyNames)
            {
                MessagePolicies.Add(new MessagePolicy(typeof(TMessage), policyName));
            }
        }

        public string[] GetMessagePolicies(Type messageType)
        {
            return MessagePolicies
                .Where(p => p.MessageType == messageType)
                .Select(p => p.PolicyName)
                .Distinct()
                .ToArray();
        }

        public void ConfigureAuthorization(Action<AuthorizationOptions> configure)
        {
            configure(Authorization.Options);
        }
    }
}
