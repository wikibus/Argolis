﻿using System;
using System.Linq;
using Argolis.Hydra;
using Argolis.Hydra.Resources;
using Argolis.Models;
using Argolis.Nancy;
using JsonLD.Entities;
using Nancy.ModelBinding;
using TestHydraApi;

namespace TestHydraApp.Modules
{
    public sealed class IssuesModule : ArgolisModule
    {
        private readonly IIriTemplateFactory templateFactory;

        public IssuesModule(IIriTemplateFactory templateFactory, IModelTemplateProvider provider)
            : base(provider)
        {
            this.templateFactory = templateFactory;
            using (this.Templates)
            {
                this.Get<Issue>(_ => new Issue
                {
                    Id = this.Request.Url,
                    Content = "This Hydra library is not yet complete",
                    DateCreated = new DateTime(2016, 3, 21),
                    IsResolved = _.id % 2 == 0,
                    ProjectId = (IriRef)"/project/argolis",
                    Submitter = new User { Name = "Tomasz", LastName = "Pluskiewicz" },
                    Title = "Complete implementation"
                });

                this.Get<Collection<Issue>>(_ => this.StubCollection(this.Bind<IssueFilter>()));
            }
        }

        private Collection<Issue> StubCollection(IssueFilter filter)
        {
            var random = new Random();

            var members = Enumerable.Range(1, 10).Select(i => new Issue
            {
                Title = $"{filter.Title} issue #{i}",
                DateCreated = new DateTime(filter.Year ?? random.Next(2000, DateTime.Now.Year), random.Next(1, 12), random.Next(1, 29)),
                ProjectId = (IriRef)"/ęś"
            });

            return new IssueCollection
            {
                Id = this.Request.Url,
                Members = members.ToArray(),
                TotalItems = 10,
                Search = this.templateFactory.CreateIriTemplate<IssueFilter, Issue>()
            };
        }
    }
}