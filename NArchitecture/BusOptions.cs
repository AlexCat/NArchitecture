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
        public EventOptions EventOptions { get; }
        public RequestOptions RequestOptions { get; }
        public AuthorizationOptions AuthorizationOptions { get; }
        public IList<IMessagePolicy> MessagePolicies { get; }

        public BusOptions()
        {
            EventOptions = new EventOptions();
            RequestOptions = new RequestOptions();
            AuthorizationOptions = new AuthorizationOptions();
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

        public void ConfigureEvents(Action<EventOptions> configure)
        {
            configure(EventOptions);
        }

        public void ConfigureRequests(Action<RequestOptions> configure)
        {
            configure(RequestOptions);
        }

        public void ConfigureAuthorization(Action<AuthorizationOptions> configure)
        {
            configure(AuthorizationOptions);
        }
    }
}
