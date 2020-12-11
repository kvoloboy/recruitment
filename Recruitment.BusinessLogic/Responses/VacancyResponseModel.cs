using System;

namespace Recruitment.BusinessLogic.Responses
{
    public class VacancyResponseModel
    {
        public Guid Id { get; set; }

        public string Position { get; set; }

        public string ShortDescription { get; set; }

        public string Domain { get; set; }

        public double MinSalary { get; set; }

        public double MaxSalary { get; set; }

        public int MinExperienceInYears { get; set; }

        public string[] Keywords { get; set; }

        public string[] Skills { get; set; }

        public bool IsActive { get; set; }
    }
}
