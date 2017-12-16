using Argolis.Models;
using Nancy;
using Nancy.Bootstrapper;

namespace Argolis.Nancy
{
    /// <summary>
    /// Registers defaults with Nancy's container
    /// </summary>
    public class Installer : Registrations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Installer"/> class.
        /// </summary>
        public Installer(ITypeCatalog catalog)
            : base(catalog)
        {
            this.RegisterWithDefault<IModelTemplateProvider>(typeof(AttributeModelTemplateProvider));
        }
    }
}