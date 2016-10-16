﻿using NArchitecture.Requests;
using System.Threading.Tasks;

namespace NArchitecture.Tests.Requests
{
    public class ComplexRequestHandler : RequestHandler<ComplexRequest, int>
    {
        protected override Task<int> Handle(ComplexRequest request)
        {
            return Task.FromResult(0);
        }
    }
}