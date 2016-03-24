using System.Collections.Generic;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.Security;

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
            public IUserIdentity Validate(string username, string password)
            {
                if (password == "P@ssw0rD")
                {
                    return new TestUser(username);
                }

                return null;
            }
        }

        public class TestUser : IUserIdentity
        {
            public TestUser(string userName)
            {
                UserName = userName;
                Claims = new[]
                {
                    userName
                };
            }

            public string UserName { get; }

            public IEnumerable<string> Claims { get; }
        }
    }
}