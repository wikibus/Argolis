using System.ComponentModel;
using Hydra.Annotations;

namespace TestHydraApi
{
    [SupportedClass("http://example.api/o#Issue")]
    [Description("An issue reported by our users")]
    public class Issue : IssueBase
    {
        public string Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }
        
        [Description("The number of people who liked this issue")]
        public int LikesCount { get; private set; }

        public bool IsResolved { get; set; }

        public User Submitter { get; set; }

        [Range("http://example.api/o#project")]
        public string ProjectId { get; set; }

        public UndocumentedClass UndocumentedClassProperty { get; set; }
    }
}
