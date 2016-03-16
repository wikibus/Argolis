using Hydra.Annotations;

namespace TestHydraApi
{
    [SupportedClass("http://example.api/o#Issue")]
    public class Issue
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
