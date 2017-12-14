using System;
using System.Linq;
using Argolis.Hydra;
using JsonLD.Entities;
using Nancy.Routing.UriTemplates;
using TestHydraApi;
using Nancy.ModelBinding;

namespace TestNancyApp.Modules
{
    public sealed class IssuesModule : UriTemplateModule
    {
        private const string Base = "http://localhost:61186";
        private readonly IIriTemplateFactory templateFactory;

        public IssuesModule(IIriTemplateFactory templateFactory) : base("issues")
        {
            this.templateFactory = templateFactory;
            Get("{id}", _ => new Issue
            {
                Id = Request.Url,
                Content = "This Hydra library is not yet complete",
                DateCreated = new DateTime(2016,3,21),
                IsResolved = _.id % 2 == 0,
                ProjectId = (IriRef)"/project/argolis",
                Submitter = new User { Name = "Tomasz", LastName = "Pluskiewicz" },
                Title = "Complete implementation"
            });

            Get("", _ => StubCollection(this.Bind<IssueFilter>()));
        }

        private IssueCollection StubCollection(IssueFilter filter)
        {
            var random = new Random();

            var members = Enumerable.Range(1, 10).Select(i => new Issue
            {
                Title = $"{filter.Title} issue #{i}",
                DateCreated = new DateTime(filter.Year ?? random.Next(2000, DateTime.Now.Year), random.Next(1, 12), random.Next(1, 29)),
                ProjectId = (IriRef) "/ęś"
            });

            return new IssueCollection
            {
                Id = Request.Url,
                Members = members.ToArray(),    
                TotalItems = 10,
                Search = this.templateFactory.CreateIriTemplate<IssueFilter>($"{Base}/issues")
            };
        }
    }
}