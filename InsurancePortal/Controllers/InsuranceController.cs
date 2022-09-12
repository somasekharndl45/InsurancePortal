using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InsurancePortal.DataEntities;
using InsurancePortal.Services;
using InsurancePortal.Model;

namespace InsurancePortal.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InsuranceController : ControllerBase
    {
        public readonly IMemberService _memberService;
        private InsuranceContext dbContext { get; set; }

        public InsuranceController(IMemberService memberService, InsuranceContext insuranceContext)
        {
            _memberService = memberService;
            dbContext = insuranceContext;
        }
        string result = string.Empty;

        [HttpPost]
        public ActionResult CreateMembers([FromBody] UserRegistration userRegistration)
        {
            try
            {
                 result = _memberService.CreateMember(userRegistration);  
            }
            catch (Exception ex)
            {
            result=  ex.Message;
            }
            return Ok(new
            {
                Result = result
            });
        }

        [HttpPost]
        public ActionResult<string> Login([FromBody] Credentials credentials)
        {
            string result = _memberService.UserLogin(credentials);
            return Ok(new
            {
                Result = result
            }); 
        }

        [HttpPost]
        public ActionResult Members([FromBody] MemberRegistration memberRegistration)
        {
            try
            {
                result = _memberService.MemberRegistrations(memberRegistration);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            //return Ok(result);
            return Ok(new
            {
                Result = result
            });
        }
        [HttpGet("GetById")]
        public ActionResult<List<object>> SearchUser(string? memberId, string? firstname, string? lastname)
        {
            string result = string.Empty;
            try
            {
                List<object> list = new List<object>();
                object mr = _memberService.GetById(Convert.ToInt32(memberId), firstname, lastname);
                list.Add(mr);
                if (mr != null)
                    return Ok(list);
                else

                    result = "Member not Found";

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Ok(result.ToList());
        }

        [HttpPost]
        public ActionResult CreateClaimSubmission([FromBody] PolicySubmission policySubmission)
        {
            try
            {
                result = _memberService.CreatePolicy(policySubmission);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return Ok(new
            {
                Result = result
            });
        }
    }
    }

