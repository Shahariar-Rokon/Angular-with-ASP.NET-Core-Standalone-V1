using Microsoft.EntityFrameworkCore;

namespace ResumeManagement.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        { }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> skills { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { SkillId = 1, SkillName = "C#" });
        }
        internal object Find(int CandidateId)
        {
            throw new NotImplementedException();
        }
    }
}
