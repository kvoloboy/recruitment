using System;

namespace Recruitment.BusinessLogic.Requests.Vacancy
{
    public class FilterVacancyRequest
    {
        public string Domain { get; set; }

        public string Position { get; set; }

        public Guid[] Skills { get; set; }
    }
}
