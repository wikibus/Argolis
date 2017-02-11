using System;
using System.Collections.Generic;
using System.Security.Claims;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;

namespace TestNancyApp.Bootstrap
{
    public class AuthenticationStartup : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
        {
            BasicAuthentication.Enable(pipelines, new BasicAuthenticationConfiguration(new DummyValidator(), "argolis"));
        }

        public class DummyValidator : IUserValidator
        {
            public ClaimsPrincipal Validate(string username, string password)
            {
                if (password == "P@ssw0rD")
                {
                    return new TestUser(username);
                }

                return null;
            }
        }

        public class TestUser : ClaimsPrincipal
        {
            public TestUser(string userName)
            {
                UserName = userName;
                Claims = new[]
                {
                    new Claim(ClaimTypes.Name, userName) 
                };
            }

            public string UserName { get; }

            public override IEnumerable<Claim> Claims { get; }
        }
    }
}