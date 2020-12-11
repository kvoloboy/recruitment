using System;

namespace Recruitment.Domain.Models.Entities
{
    public class VacancyApplication
    {
        public Guid VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
