using System;
using System.ComponentModel;
using Argolis.Hydra.Annotations;

namespace TestHydraApi
{
    public class IssueBase
    {
        [ReadOnly(true)]
        public DateTime DateCreated { get; set; }

        [Writeable(false)]
        public DateTime? DateDeleted { get; set; }
    }
}