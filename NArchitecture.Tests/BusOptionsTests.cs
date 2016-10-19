using System;
using Xunit;

namespace NArchitecture.Tests
{
    public class BusOptionsTests
    {
        [Fact(DisplayName = "BusOptions can bind policy to message type")]
        public void AddPolicyForTest()
        {
            var options = new BusOptions();
            options.AddPolicyFor<IMessage>("CustomPolicy");
            Assert.Equal("CustomPolicy", options.GetPolicyFor(typeof(IMessage)));
        }

        [Fact(DisplayName = "BusOptions return null policy for unknown message type")]
        public void GetPolicyForTest()
        {
            var options = new BusOptions();
            Assert.Null(options.GetPolicyFor(typeof(IMessage)));
        }

        [Fact(DisplayName = "BusOptions throws if message bound to empty policy")]
        public void AddEmptyPolicyForTest()
        {
            var options = new BusOptions();
            Assert.Throws<ArgumentException>(() =>
            {
                options.AddPolicyFor<IMessage>("");
            });
        }
    }
}
