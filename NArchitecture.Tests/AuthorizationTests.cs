﻿using FakeItEasy;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class AuthorizationTests
    {
        private class SuccessfulRequirement : IAuthorizationRequirement { }

        private class SuccessfulAuthorizationHandler : AuthorizationHandler<SuccessfulRequirement>
        {
            protected override Task Handle(AuthorizationHandlerContext context, SuccessfulRequirement requirement)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }
        }

        [Fact(DisplayName = "AuthorizationService returns true if handler succeeds")]
        public async Task SucceededRequirementTest()
        {
            var user = A.Fake<ClaimsPrincipal>();
            var handler = new SuccessfulAuthorizationHandler();
            var requirement = new SuccessfulRequirement();
            var message = A.Fake<IMessage>();
            var options = new AuthorizationOptions();
            options.AddPolicy("CustomPolicy", p => p.AddRequirements(requirement));
            var service = new DefaultAuthorizationService(options, new IAuthorizationHandler[] { handler });

            Assert.True(await service.Authorize(user, message, "CustomPolicy"));
        }

        private class EmptyRequirement : IAuthorizationRequirement { }

        private class EmptyAuthorizationHandler : AuthorizationHandler<EmptyRequirement>
        {
            protected override Task Handle(AuthorizationHandlerContext context, EmptyRequirement requirement)
            {
                return Task.FromResult(0);
            }
        }

        [Fact(DisplayName = "AuthorizationService returns false if handler does not succeeds")]
        public async Task FailedRequirementTest()
        {
            var user = A.Fake<ClaimsPrincipal>();
            var handler = new EmptyAuthorizationHandler();
            var requirement = new EmptyRequirement();
            var message = A.Fake<IMessage>();
            var options = new AuthorizationOptions();
            options.AddPolicy("CustomPolicy", p => p.AddRequirements(requirement));
            var service = new DefaultAuthorizationService(options, new IAuthorizationHandler[] { handler });

            Assert.False(await service.Authorize(user, message, "CustomPolicy"));
        }

        [Fact(DisplayName = "AuthorizationService throws exception if there is no policy")]
        public async Task NoRequirementTest()
        {
            var user = A.Fake<ClaimsPrincipal>();
            var message = A.Fake<IMessage>();
            var options = new AuthorizationOptions();
            var service = new DefaultAuthorizationService(options, new IAuthorizationHandler[0]);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return service.Authorize(user, message, "CustomPolicy");
            });
        }
    }
}
