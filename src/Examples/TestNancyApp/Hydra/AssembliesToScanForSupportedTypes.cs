using System.Collections.Generic;
using System.Reflection;
using Argolis.Hydra.Discovery.SupportedClasses;
using TestHydraApi;

namespace TestNancyApp.Hydra
{
    public class AssembliesToScanForSupportedTypes : AssemblyAnnotatedTypeSelector
    {
        protected override IEnumerable<Assembly> Assemblies
        {
            get { yield return typeof (Issue).Assembly; }
        }
    }
}