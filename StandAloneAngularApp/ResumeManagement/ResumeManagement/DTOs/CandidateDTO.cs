using ResumeManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResumeManagement.DTOs
{
    public class CandidateDTO
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime DateOfBirth { get; set; }
        public string MobileNo { get; set; }
        public IFormFile PictureFile { get; set; }
        public bool IsFresher { get; set; }
        public string  SkillStringify { get; set; }
        public Skill[]?  SkillList { get; set; }
        public string? Picture { get; set; }
    }
}
