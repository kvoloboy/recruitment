using System;
using System.Collections.Generic;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.Domain.Models.Entities
{
    public class Candidate : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string Position { get; set; }

        public double ExpectedSalary { get; set; }

        public int ExperienceInYears { get; set; }

        public string Domain { get; set; }

        public Guid[] Skills { get; set; }

        public ICollection<VacancyApplication> VacancyApplications { get; set; }

        public ICollection<QuestionnaireResult> QuestionnaireResults { get; set; }
    }
}
