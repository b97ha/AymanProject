using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

    // Replace ONLY these two classes in your Models/EvaluationContext.cs:

    public class ProjectMainCriterian
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int MainCriterianId { get; set; }

        [Required]
        [Range(0.0, 100.0, ErrorMessage = "Evaluation must be between 0 and 100")]
        [Display(Name = "User Evaluation")]
        public double UserEvaluation { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("MainCriterianId")]
        public virtual MainCriterian MainCriterian { get; set; }

        public virtual ICollection<ProjectSubCriterian> ProjectSubCriterians { get; set; } = new List<ProjectSubCriterian>();
        public double CalculateScore()
        {
            double totalScore = 0;

            if (ProjectSubCriterians != null && ProjectSubCriterians.Any())
            {
                // Sum all sub-criterion scores
                foreach (var sub in ProjectSubCriterians)
                {
                    // Verified formula: (main_weight * main_eval) * (sub_weight * sub_eval) / 100
                    double mainWeight = MainCriterian.Weight;      // Should be decimal like 0.269
                    double mainEval = UserEvaluation;              // Should be percentage like 100
                    double subWeight = sub.SubCriterian.Weight;    // Should be decimal like 0.331
                    double subEval = sub.UserEvaluation;          // Should be percentage like 100

                    double subScore = (mainWeight * mainEval) * (subWeight * subEval) / 100.0;
                    totalScore += subScore;

                    // Debug output (remove in production)
                    Console.WriteLine($"Sub-criterion: ({mainWeight:F3} × {mainEval}) × ({subWeight:F3} × {subEval}) ÷ 100 = {subScore:F3}");
                }
            }
            else
            {
                // No sub-criteria: just main weight × main evaluation
                totalScore = MainCriterian.Weight * UserEvaluation;
            }

            return Math.Round(totalScore, 3); // Round to avoid floating point precision issues
        }
    }

    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(500, ErrorMessage = "Title cannot exceed 500 characters")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Submitted date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Submitted On")]
        public DateTime SubmittedOn { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "End On")]
        public DateTime EndOn { get; set; }

        public virtual ICollection<ProjectMainCriterian> ProjectMainCriterians { get; set; } = new List<ProjectMainCriterian>();

        [NotMapped]
        public double? TotalScore
        {
            get
            {
                if (ProjectMainCriterians == null || !ProjectMainCriterians.Any())
                    return null;

                // Calculate the sum of all main criteria scores
                double totalScore = 0;

                foreach (var pmc in ProjectMainCriterians)
                {
                    totalScore += pmc.CalculateScore();
                }

                // IMPORTANT: Return the actual calculated score, not multiplied by anything
                return Math.Round(totalScore, 3); // Round to 3 decimal places for precision
            }
        }

        // Add a debug property to help troubleshoot
        [NotMapped]
        public string TotalScoreDebug
        {
            get
            {
                if (ProjectMainCriterians == null || !ProjectMainCriterians.Any())
                    return "No criteria found";

                var debug = new System.Text.StringBuilder();
                double total = 0;

                foreach (var pmc in ProjectMainCriterians)
                {
                    var score = pmc.CalculateScore();
                    debug.AppendLine($"Main Criterion {pmc.MainCriterianId}: {score:F3}");
                    total += score;
                }

                debug.AppendLine($"TOTAL: {total:F3}");
                return debug.ToString();
            }
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

