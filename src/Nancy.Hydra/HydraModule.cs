namespace Nancy.Hydra
{
    /// <summary>
    /// Server Hydra API documentation
    /// </summary>
    public class HydraModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraModule"/> class.
        /// </summary>
        public HydraModule() : base("doc")
        {
            Get["/"] = route => CreateApiDocumentation();
        }

        private ApiDocumentation CreateApiDocumentation()
        {
            return new WikibusApi();
        }
    }
}
