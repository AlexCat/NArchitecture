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
        public IList<IMessageAuthorization> MessageAuthorizations { get; }

        public BusOptions()
        {
            Events = new EventComposition();
            Requests = new RequestComposition();
            Authorization = new AuthorizationComposition();
            MessageAuthorizations = new List<IMessageAuthorization>();
        }

        public void AddMessageAuthorization<TMessage>(string policy)
            where TMessage : IMessage
        {
            MessageAuthorizations.Add(new MessageAuthorization(typeof(TMessage), policy, null));
        }

        public IMessageAuthorization[] GetMessageAuthorization(Type messageType)
        {
            return MessageAuthorizations
                .Where(p => p.MessageType == messageType)
                .ToArray();
        }

        public void ConfigureAuthorization(Action<AuthorizationOptions> configure)
        {
            configure(Authorization.Options);
        }
    }
}
