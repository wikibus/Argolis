using System;
using System.ComponentModel;
using Hydra.Annotations;

namespace TestHydraApi
{
    [SupportedClass("http://example.api/o#Issue")]
    [Description("An issue reported by our users")]
    public class Issue
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        
        [Description("The number of people who liked this issue")]
        public int LikesCount { get; private set; }

        [ReadOnly(true)]
        public DateTime DateCreated { get; set; }

        public DateTime? DateDeleted { get; set; }

        public bool IsResolved { get; set; }
    }
}
