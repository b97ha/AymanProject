using Microsoft.EntityFrameworkCore;

namespace AymanProject.Models
{

    public class EvaluationContext : DbContext
    {
        public DbSet<MainCriterian> MainCriterians { get; set; }
        public DbSet<SubCriterian> SubCriterians { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSubCriterian> ProjectSubCriterians { get; set; }
        public DbSet<ProjectMainCriterian> ProjectMainCriterians { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=evaluation.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure MainCriterian entity
            modelBuilder.Entity<MainCriterian>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).ValueGeneratedNever(); // We're setting IDs manually
                entity.Property(m => m.Text_Ar).IsRequired();
                entity.Property(m => m.Text_En).IsRequired();
                entity.Property(m => m.Weight).IsRequired();
            });

            // Configure SubCriterian entity
            modelBuilder.Entity<SubCriterian>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd(); // Auto-increment for sub criteria
                entity.Property(s => s.Text_Ar).IsRequired();
                entity.Property(s => s.Text_En).IsRequired();
                entity.Property(s => s.Weight).IsRequired();

                // Configure the relationship with Fluent API
                entity.HasOne(s => s.MainCriterian)
                    .WithMany(m => m.SubCriterians)
                    .HasForeignKey(s => s.MainId)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete if main is deleted
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd(); // Auto-increment for sub criteria
                entity.Property(s => s.Title).IsRequired();
                entity.Property(s => s.Location).IsRequired();
            });


            // Configure  entity
            modelBuilder.Entity<ProjectMainCriterian>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd(); // Auto-increment for sub criteria


                // Configure the relationship with Fluent API
                entity.HasOne(s => s.MainCriterian)
                    .WithMany()
                    .HasForeignKey(s => s.MainCriterianId)
                    .OnDelete(DeleteBehavior.Restrict); // Cascade delete if main is deleted


                // Configure the relationship with Fluent API
                entity.HasOne(s => s.Project)
                    .WithMany(m => m.ProjectMainCriterians)
                    .HasForeignKey(s => s.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

            });


            // Configure  entity
            modelBuilder.Entity<ProjectSubCriterian>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd(); // Auto-increment for sub criteria


                // Configure the relationship with Fluent API
                entity.HasOne(s => s.SubCriterian)
                    .WithMany()
                    .HasForeignKey(s => s.SubCriterianId)
                    .OnDelete(DeleteBehavior.Restrict); // Cascade delete if main is deleted


                // Configure the relationship with Fluent API
                entity.HasOne(s => s.ProjectMainCriterian)
                    .WithMany(m => m.ProjectSubCriterians)
                    .HasForeignKey(s => s.ProjectMainCriterianId)
                    .OnDelete(DeleteBehavior.Cascade);


            });



        }

    }

    public class MainCriterian
    {
        public int Id { get; set; }
        public string Text_Ar { get; set; }
        public string Text_En { get; set; }
        public double Weight { get; set; } // Changed to double to match the decimal weights
        public ICollection<SubCriterian> SubCriterians { get; set; }
    }

    public class SubCriterian
    {
        public int Id { get; set; }
        public int MainId { get; set; }
        public string Text_Ar { get; set; }
        public string Text_En { get; set; }
        public double Weight { get; set; } // Changed to double to match the decimal weights
        public MainCriterian MainCriterian { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime SubmittedOn { get; set; }
        public DateTime EndOn { get; set; }

        public ICollection<ProjectMainCriterian> ProjectMainCriterians { get; set; }
    }

    public class ProjectMainCriterian
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int MainCriterianId { get; set; }
        public MainCriterian MainCriterian { get; set; }
        public double UserEvaluation { get; set; }
        public ICollection<ProjectSubCriterian> ProjectSubCriterians { get; set; }

        public double CalculateScore()
        {
            double score = (MainCriterian.Weight * UserEvaluation) / 100.0;
            if (ProjectSubCriterians != null && ProjectSubCriterians.Any())
            {
                foreach (var sub in ProjectSubCriterians)
                {
                    score += (sub.SubCriterian.Weight * sub.UserEvaluation) / 100.0;
                }
            }
            return score;
        }
    }

    public class ProjectSubCriterian
    {
        public int Id { get; set; }
        public int ProjectMainCriterianId { get; set; }
        public ProjectMainCriterian ProjectMainCriterian { get; set; }
        public int SubCriterianId { get; set; }
        public SubCriterian SubCriterian { get; set; }
        public double UserEvaluation { get; set; }
    }

}

