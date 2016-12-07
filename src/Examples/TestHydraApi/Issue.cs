using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Argolis.Hydra.Annotations;
using JsonLD.Entities;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestHydraApi
{
    [SupportedClass(IssueType)]
    [Description("An issue reported by our users")]
    public class Issue : IssueBase
    {
        private const string IssueType = "http://example.api/o#Issue";

        public string Id { get; set; }
        
        [JsonProperty("titel")]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        [Description("The number of people who liked this issue")]
        public int LikesCount { get; private set; }

        public bool IsResolved { get; set; }

        public User Submitter { get; set; }

        [Argolis.Hydra.Annotations.Range("http://example.api/o#project")]
        public IriRef ProjectId { get; set; }

        public UndocumentedClass UndocumentedClassProperty { get; set; }

        public string Method()
        {
            return null;
        }

        [JsonProperty]
        private string Type
        {
            get { return IssueType; }
        }

        private static JToken Context
        {
            get
            {
                return new JArray(
                    new AutoContext<Issue>(new Uri(IssueType))
                        .Property(i => i.ProjectId, builder => builder.Type().Id()),
                    User.Context
                    );
            }
        }
    }
}
