using System.Collections.Generic;
using System.Reflection;
using Hydra.Discovery.SupportedClasses;
using TestHydraApi;

namespace TestNancyApp
{
    public class AssembliesToScanForSupportedTypes : AssemblyAnnotatedTypeSelector
    {
        protected override IEnumerable<Assembly> Assemblies
        {
            get { yield return typeof (Issue).Assembly; }
        }
    }
}