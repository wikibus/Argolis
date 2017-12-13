using Hydra.Annotations;
using Vocab;

namespace TestHydraApi
{
    public class IssueFilter
    {
        [Property(Schema.title)]
        public string Title { get; set; }

        [Property(Schema.BaseUri + "year")]
        [Range(Xsd.integer)]
        public int? Year { get; set; }
    }
}