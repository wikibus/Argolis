using System;
using System.ComponentModel;

namespace TestHydraApi
{
    public class IssueBase
    {
        [ReadOnly(true)]
        public DateTime DateCreated { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}