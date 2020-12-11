using System;

namespace Recruitment.BusinessLogic.Requests.Candidate
{
    public class CandidateFilterRequest
    {
        public string Position { get; set; }

        public double? MinExpectedSalary { get; set; }

        public double? MaxExpectedSalary { get; set; }

        public int? ExperienceInYears { get; set; }

        public string Domain { get; set; }

        public Guid[] Skills { get; set; }
    }
}
