using System;
using JsonLD.Entities;
using Nancy;
using TestHydraApi;

namespace TestNancyApp.Modules
{
    public class IssuesModule : NancyModule
    {
        public IssuesModule() : base("issues")
        {
            Get["{id}"] = _ => new Issue
            {
                Id = Request.Url,
                Content = "This Hydra library is not yet complete",
                DateCreated = new DateTime(2016,3,21),
                IsResolved = _.id % 2 == 0,
                ProjectId = (IriRef)"/project/argolis",
                Submitter = new User { Name = "Tomasz", LastName = "Pluskiewicz" },
                Title = "Complete implementation"
            };
        }
    }
}