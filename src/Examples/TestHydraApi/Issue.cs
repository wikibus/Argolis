using System.ComponentModel;
using Hydra.Annotations;
using Newtonsoft.Json;

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
        
        public string Content { get; set; }
        
        [Description("The number of people who liked this issue")]
        public int LikesCount { get; private set; }

        public bool IsResolved { get; set; }

        public User Submitter { get; set; }

        [Range("http://example.api/o#project")]
        public string ProjectId { get; set; }

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
    }
}
