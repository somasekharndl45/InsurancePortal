using System;
using System.Collections.Generic;

namespace InsurancePortal.DataEntities
{
    public partial class PolicySubmission
    {
        public int PolicyId { get; set; }
        public string? PolicyStatus { get; set; }
        public string? PolicyType { get; set; }
        public decimal? PremiumAmount { get; set; }
        public DateTime? PolicyEffectiveDate { get; set; }
        public int? MemberId { get; set; }
        public string? Remark { get; set; }

        //public virtual MemberRegistration? Member { get; set; }
    }
}
