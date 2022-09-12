using InsurancePortal.DataEntities;
using InsurancePortal.Model;

namespace InsurancePortal.Services
{
    public interface IMemberService
    {
        InsuranceContext dbContext { get; set; }

        string CreateMember(UserRegistration userRegistration);
        string UserLogin(Credentials credentials);
        string MemberRegistrations(MemberRegistration memberRegistration);
        List<MemberRegistration> Menbersearch(MemberDetails memberDetails);
        object GetById(int memberId, string firstname, string lastname);
        string CreatePolicy(PolicySubmission policySubmission);
    }
}