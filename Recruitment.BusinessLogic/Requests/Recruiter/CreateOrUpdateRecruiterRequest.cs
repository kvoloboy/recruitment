using System.ComponentModel.DataAnnotations;

namespace Recruitment.BusinessLogic.Requests.Recruiter
{
    public class CreateOrUpdateRecruiterRequest
    {
        [Required]
        public string Company { get; set; }

        [Required]
        public string Position { get; set; }
    }
}
