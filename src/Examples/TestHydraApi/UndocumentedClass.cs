using Argolis.Hydra.Annotations;

namespace TestHydraApi
{
    /// <summary>
    /// This class will not be included in ApiDocumentation
    /// </summary>
    public class UndocumentedClass
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string NoSetter
        {
            get { return "some test"; }
        }

        public string NoGetter
        {
            set { }
        }

        [WriteOnly]
        public string WriteOnly { get; set; }
    }
}