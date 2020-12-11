using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recruitment.DataAccess.Extensions;
using Recruitment.Domain.Models.Entities;

namespace Recruitment.DataAccess.Configurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.Property(vacancy => vacancy.Keywords)
                .HasJsonConversion();

            builder.Property(result => result.Skills)
                .HasJsonConversion();
        }
    }
}
