using System;
using Xunit;

namespace NArchitecture.Tests
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void GetHandlerServiceTypesForEventHandler()
        {
            var serviceTypes = typeof(SimpleEventHandler).GetHandlerServiceTypes();
            Assert.Equal(new Type[] { typeof(IHandleEvent<SimpleEvent>) }, serviceTypes);
        }

        [Fact]
        public void GetHandlerServiceTypesForRequestHandler()
        {
            var serviceTypes = typeof(SimpleRequestHandler).GetHandlerServiceTypes();
            Assert.Equal(new Type[] { typeof(IHandleRequest<SimpleRequest>) }, serviceTypes);
        }

        [Fact]
        public void GetHandlerServiceTypesForRequestHandlerWithResponse()
        {
            var serviceTypes = typeof(ComplexRequestHandler).GetHandlerServiceTypes();
            Assert.Equal(new Type[] { typeof(IHandleRequest<ComplexRequest, int>) }, serviceTypes);
        }

        [Fact]
        public void GetHandlerServiceTypesForUnkownHandler()
        {
            var serviceTypes = typeof(UnknownHandler).GetHandlerServiceTypes();
            Assert.Equal(new Type[0], serviceTypes);
        }
    }
}
