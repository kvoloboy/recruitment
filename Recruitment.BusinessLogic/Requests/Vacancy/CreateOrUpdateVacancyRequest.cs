using System;
using System.ComponentModel.DataAnnotations;

namespace Recruitment.BusinessLogic.Requests.Vacancy
{
    public class CreateOrUpdateVacancyRequest
    {
        [Required]
        public string Position { get; set; }

        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Domain { get; set; }

        public double MinSalary { get; set; }

        public double MaxSalary { get; set; }

        public int MinExperienceInYears { get; set; }

        public string[] Keywords { get; set; }

        public Guid[] Skills { get; set; }

        public Guid[] Questionnaires { get; set; } = { };
    }
}
