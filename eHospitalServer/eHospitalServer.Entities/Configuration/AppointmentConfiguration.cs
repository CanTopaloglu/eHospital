using eHospitalServer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Entities.Configuration;
internal sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(p => p.Price).HasColumnType("money");
        builder.HasQueryFilter(filter => (!filter.Doctor!.IsDeleted || !filter.Patient!.IsDeleted) && !filter.IsItFinished);
    }
}
