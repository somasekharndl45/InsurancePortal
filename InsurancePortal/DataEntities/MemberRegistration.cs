using System;
using System.Collections.Generic;

namespace InsurancePortal.DataEntities
{
    public partial class MemberRegistration
    {
        public MemberRegistration()
        {
            PolicySubmissions = new HashSet<PolicySubmission>();
        }

        public int MemberId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<PolicySubmission> PolicySubmissions { get; set; }
    }
}
