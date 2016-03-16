using Hydra.Annotations;

namespace TestHydraApi
{
    [SupportedClass]
    public class Issue
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
