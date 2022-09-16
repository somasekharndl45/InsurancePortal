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
                if (dbContext.UserRegistrations.Where(x => x.UserName == userRegistration.UserName).Count() > 0)
                {
                    return "UserName already exists";
                }

                UserRegistration register = new UserRegistration();
                register.UserName = userRegistration.UserName;
                register.Password = userRegistration.Password;
                register.UserRole = userRegistration.UserRole;

                dbContext.UserRegistrations.Add(register);
                dbContext.SaveChanges();
                return "Registration Succesfull";
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

        public object GetById(int memberId,string firstname,string lastname, string policystatus,int policyid)
        {
            try
            {
                if(policystatus == "" || policystatus == null)
                {
                    policystatus = "qwerty";
                }
                var result = dbContext.MemberRegistrations.Where(m => m.MemberId == memberId).FirstOrDefault();
                var entity = from m in dbContext.MemberRegistrations
                             join p in dbContext.PolicySubmissions
                             on m.MemberId equals p.MemberId into ab
                             from t in ab.DefaultIfEmpty()
                             where m.MemberId == memberId ||
                            (m.FirstName == firstname &&
                             m.LastName == lastname) ||
                            t.PolicyStatus == policystatus ||
                            t.PolicyId == policyid

                             select new
                             {
                                 memberId = m.MemberId,
                                 PolicyId = t.PolicyId == null ? 0 : (t.PolicyId),
                                 UserName = m.UserName,
                                 FirstName = m.FirstName,
                                 LastName = m.LastName,
                                 policyStatus = (t.PolicyStatus == null || t.PolicyStatus == "") ? "No Policy Found" : t.PolicyStatus,
                                 policyType = t.PolicyType,
                                 premiumAmount = t.PremiumAmount,
                                 PolicyEffectiveDate = DateTime.Now.ToString("yyyy/MM/dd")
                                 //t.PolicyEffectiveDate == null ? DateTime.Now.ToString("yyyy/MM/dd") : t.PolicyEffectiveDate.ToString("yyyy/MM/dd")

                                 //PolicyEffectiveDate = t.PolicyEffectiveDate == null ? DateTime.Now.ToString("yyyy/MM/dd") : t.PolicyEffectiveDate.ToString("yyyy/MM/dd");
                             };
                if (entity.Count() > 0 )
                { 
                    return entity; 
                }
                   
                else
                {
                    return "No user found";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
           
        }

        public string CreatePolicy(PolicySubmission policySubmission)
        {
            try
            {
                if (dbContext.PolicySubmissions.Where(x => x.PolicyType == policySubmission.PolicyType && x.MemberId == policySubmission.MemberId).Count() > 0)
                {
                    return $"Policy already exists with memberId  {policySubmission.MemberId}";
                }
                PolicySubmission policy = new PolicySubmission();
                policy.PolicyStatus = policySubmission.PolicyStatus;
                policy.PolicyType = policySubmission.PolicyType;
                policy.PolicyEffectiveDate = policySubmission.PolicyEffectiveDate;
                policy.PremiumAmount = policySubmission.PremiumAmount;
                policy.MemberId = policySubmission.MemberId;
                policy.Remark = policySubmission.Remark;

                    dbContext.PolicySubmissions.Add(policySubmission);
                    dbContext.SaveChanges();
                return $"Policy is added succesfully for {policySubmission.PolicyId}";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message; 
            }
        }

        public string UpdatePolicy(PolicySubmission policySubmission)
        {
            try
            {
                var result = dbContext.PolicySubmissions.Where(x => x.MemberId == policySubmission.MemberId).FirstOrDefault();
                string message = string.Empty;
                if (result != null)
                {
                    result.PolicyStatus = policySubmission.PolicyStatus;
                    result.PolicyType = policySubmission.PolicyType;
                    result.PremiumAmount = policySubmission.PremiumAmount;
                    result.PolicyEffectiveDate = policySubmission.PolicyEffectiveDate;
                    result.Remark = policySubmission.Remark;
                    dbContext.PolicySubmissions.Update(result);
                    dbContext.SaveChanges();
                    return $"Policy is Updated succesfully for {result.PolicyId}";
                }
                else
                {
                    return "InValid input";
                }
            }
            catch(Exception ex)
            {
                return "Error occurred in Update";
            }
            
        }
    }   
 }
   

