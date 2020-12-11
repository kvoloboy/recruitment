using System;

namespace Recruitment.BusinessLogic.Requests.Candidate
{
    public class CreateOrUpdateCandidateRequest
    {
        public string Position { get; set; }

        public double ExpectedSalary { get; set; }

        public int ExperienceInYears { get; set; }

        public string Domain { get; set; }

        public Guid[] Skills { get; set; }
    }
}
