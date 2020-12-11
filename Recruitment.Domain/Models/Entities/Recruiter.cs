using System;
using System.Collections.Generic;

namespace Recruitment.Domain.Models.Entities
{
    public class Recruiter : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string Company { get; set; }
        public string Position { get; set; }

        public ICollection<Vacancy> CreatedVacancies { get; set; }
    }
}
