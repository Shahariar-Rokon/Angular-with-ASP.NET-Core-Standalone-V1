using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeManagement.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        [Column(TypeName ="date")]
        public DateTime DateOfBirth { get; set; }
        public string MobileNo { get; set; }
        public string? Picture { get; set; }
        public bool IsFresher { get; set; }
    }
}
