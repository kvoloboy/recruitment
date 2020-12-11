using System;
using System.Collections.Generic;
using Recruitment.Domain.Models.Entities.QuestionnaireDomain;

namespace Recruitment.Domain.Models.Entities
{
    public class Vacancy : BaseEntity
    {
        public string Position { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Domain { get; set; }

        public double MinSalary { get; set; }

        public double MaxSalary { get; set; }

        public int MinExperienceInYears { get; set; }

        public string[] Keywords { get; set; }

        public Guid[] Skills { get; set; }

        public bool IsActive { get; set; }

        public Guid RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }

        public ICollection<VacancyApplication> VacancyApplications { get; set; }

        public ICollection<QuestionnaireToVacancyRelation> QuestionnaireToVacancyRelations { get; set; }
    }
}
