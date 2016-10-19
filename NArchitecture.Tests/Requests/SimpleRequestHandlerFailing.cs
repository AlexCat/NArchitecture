﻿using System;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    public class SimpleRequestHandlerFailing : RequestHandler<SimpleRequest>
    {
        protected override Task Handle(RequestHandlerContext context, SimpleRequest request)
        {
            throw new InvalidOperationException();
        }
    }
}
