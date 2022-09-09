using InsurancePortal.DataEntities;
using InsurancePortal.Model;

namespace InsurancePortal.Services
{
    public class MemberService : IMemberService
    {
        public InsuranceContext dbContext { get; set; }

        public MemberService(InsuranceContext insuranceContext)
        {
            dbContext = insuranceContext;
        }
        public string CreateMember(UserRegistration userRegistration)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(userRegistration.UserName) && !string.IsNullOrWhiteSpace(userRegistration.Password)
                   && !string.IsNullOrWhiteSpace(userRegistration.UserRole))
                {
                    UserRegistration register = new UserRegistration();
                    register.UserName = userRegistration.UserName;
                    register.Password = userRegistration.Password;
                    register.UserRole = userRegistration.UserRole;

                    dbContext.UserRegistrations.Add(register);
                    dbContext.SaveChanges();
                    return "Registration succesfull";
                }
                return "Enter Valid data";
            }
            catch (Exception)
            {
                return "Some error occurred";
            }
        }
        public string UserLogin(Credentials credentials)
        {
            if (!string.IsNullOrWhiteSpace(credentials.UserName) && !string.IsNullOrWhiteSpace(credentials.Password))
            {
                string message = string.Empty;
                var result = dbContext.UserRegistrations.Where(u => u.UserName == credentials.UserName
                && u.Password == credentials.Password).FirstOrDefault();
                if (result != null)
                {
                    return result.UserRole;
                }
            }
            return "Invalid credentials";
        }
        public string MemberRegistrations(MemberRegistration memberRegistration)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(memberRegistration.FirstName) && !string.IsNullOrWhiteSpace(memberRegistration.LastName)
                    && !string.IsNullOrWhiteSpace(memberRegistration.UserName)
                    && !string.IsNullOrWhiteSpace(memberRegistration.State) && !string.IsNullOrWhiteSpace(memberRegistration.Address)
                    && !string.IsNullOrWhiteSpace(memberRegistration.Email))
                {
                    MemberRegistration mr = new MemberRegistration();
                    mr.FirstName = memberRegistration.FirstName;
                    mr.LastName = memberRegistration.LastName;
                    mr.UserName = memberRegistration.UserName;
                    mr.State = memberRegistration.State;
                    mr.Email = memberRegistration.Email;
                    mr.Address = memberRegistration.Address;
                    mr.DateofBirth = memberRegistration.DateofBirth;

                    dbContext.MemberRegistrations.Add(mr);
                    dbContext.SaveChanges();
                    return "member Registration succesfull";
                }
                return "Invalid data";
            }
            catch (Exception)
            {
                return "Some error occurred";
            }
        }

        public List<MemberRegistration> Menbersearch(MemberDetails memberDetails)
        {
            
            return dbContext.MemberRegistrations.ToList().Where(x => (x.MemberId == memberDetails.MemberId) || x.UserName == memberDetails.UserName).ToList();
        }

        public object GetById(int memberId)
        {
            var result = dbContext.MemberRegistrations.Where(m => m.MemberId == memberId).FirstOrDefault();
            var entity = from m in dbContext.MemberRegistrations
                         join p in dbContext.PolicySubmissions
                         on m.MemberId equals p.MemberId into ab
                         from t in ab.DefaultIfEmpty()
                         where m.MemberId == memberId
                         select new
                         {
                             memberId = m.MemberId,
                             PolicyId = t.PolicyId == null ? 0 : (t.PolicyId),
                             UserName = m.UserName,
                             FirstName = m.FirstName,
                             LastName = m.LastName,
                             policyStatus = (t.PolicyStatus == null || t.PolicyStatus == "") ? "" : t.PolicyStatus,
                             policyType = t.PolicyType,
                             premiumAmount = t.PremiumAmount
                         };
            return entity;
        }
    }   
 }
   

