using System;
using Xunit;

namespace NArchitecture.Tests
{
    public class AuthorizationPolicyTests
    {
        [Fact(DisplayName = "AuthorizationPolicy can be created")]
        public void CreateSimplePolicyTest()
        {
            var policy = new AuthorizationPolicy(new IAuthorizationRequirement[] { new MinimumAgeRequirement(21) });
            Assert.Equal(1, policy.Requirements.Count);
        }

        [Fact(DisplayName = "AuthorizationPolicy cannot be created with null requirements")]
        public void CreateAuthorizationPolicyWithNullRequirementsTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var policy = new AuthorizationPolicy(null);
            });
        }

        [Fact(DisplayName = "AuthorizationPolicy cannot be created with empty requirements")]
        public void CreateAuthorizationPolicyWithEmptyRequirementsTest()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var policy = new AuthorizationPolicy(new IAuthorizationRequirement[0]);
            });
        }

        [Fact(DisplayName = "AuthorizationPolicies can be combined")]
        public void CombineTwoAuthorizationPoliciesTest()
        {
            var policy1 = new AuthorizationPolicy(new IAuthorizationRequirement[] { new MinimumAgeRequirement(21) });
            var policy2 = new AuthorizationPolicy(new IAuthorizationRequirement[] { new MinimumAgeRequirement(18) });
            var policy = AuthorizationPolicy.Combine(policy1, policy2);
            Assert.Equal(2, policy.Requirements.Count);
        }

        [Fact(DisplayName = "AuthorizationPolicy cannot be combined with null policy")]
        public void CombineAuthorizationPolicyWithNullTest()
        {
            var policy1 = new AuthorizationPolicy(new IAuthorizationRequirement[] { new MinimumAgeRequirement(21) });
            AuthorizationPolicy policy2 = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                var policy = AuthorizationPolicy.Combine(policy1, policy2);
            });
        }
    }
}
