using System;

namespace Recruitment.BusinessLogic.Responses
{
    public class CandidateResponseModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public double ExpectedSalary { get; set; }

        public int ExperienceInYears { get; set; }

        public string Domain { get; set; }

        public Guid[] Skills { get; set; }
    }
}
