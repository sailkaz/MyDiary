using MyDiary.Models.Domains;
using System.Data.Entity.ModelConfiguration;

namespace MyDiary.Models.Configurations
{
    public class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            ToTable("Ratings");

            HasKey(x => x.Id);
        }
    }
}
