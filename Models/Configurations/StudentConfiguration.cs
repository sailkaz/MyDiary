using MyDiary.Models.Domains;
using System.Data.Entity.ModelConfiguration;

namespace MyDiary.Models.Configurations
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("dbo.Students");

            HasKey(x => x.Id);

            Property(x => x.FirstName)
                .HasMaxLength(30)
                .IsRequired();

            Property(x => x.LastName)
                .HasMaxLength(40)
                .IsRequired();

        }
    }
}
