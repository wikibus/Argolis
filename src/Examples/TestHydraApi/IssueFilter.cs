using Argolis.Hydra.Annotations;
using Argolis.Models;
using Vocab;

namespace TestHydraApi
{
    public class IssueFilter : ITemplateParameters<Issue>
    {
        [Property(Schema.title)]
        public string Title { get; set; }

        [Property(Schema.BaseUri + "year")]
        [Range(Xsd.integer)]
        public int? Year { get; set; }
    }
}