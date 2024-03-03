using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeManagement.Models
{
    public class CandidateSkill
    {
        public int CandidateSkillId { get; set; }
        [ForeignKey(nameof(Candidate))]
        public int CandidateId { get; set; }
        [ForeignKey(nameof(Skill))]
        public int SkillId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
