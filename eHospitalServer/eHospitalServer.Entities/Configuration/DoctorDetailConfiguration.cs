using eHospitalServer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eHospitalServer.Entities.Configuration;
internal sealed class DoctorDetailConfiguration : IEntityTypeConfiguration<DoctorDetail>
{
    public void Configure(EntityTypeBuilder<DoctorDetail> builder)
    {
        builder.HasKey(key => key.Id);
        builder.Property(p => p.AppointmentPrice).HasColumnType("money");

    }
}
