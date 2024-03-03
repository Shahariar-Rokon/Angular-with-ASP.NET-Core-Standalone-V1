using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Newtonsoft.Json;
using ResumeManagement.DTOs;
using ResumeManagement.Models;

namespace ResumeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CandidatesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        [Route("GetSkill")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkill()
        {
            return await _context.skills.ToListAsync();
        }
        [HttpGet]
        [Route("GetCandidates")]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            return await _context.Candidates.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidateSkill()
        {
            List<CandidateDTO> candidateSkills= new List<CandidateDTO>();
            var allCandidates=_context.Candidates.ToList();
            foreach (var candidate in allCandidates)
            {
                var skillList = _context.CandidateSkills.Where(x => x.CandidateId == candidate.CandidateId).Select(x => new Skill { SkillId = x.SkillId }).ToList();
                candidateSkills.Add(new CandidateDTO
                {
                    CandidateId = candidate.CandidateId,
                    CandidateName = candidate.CandidateName,
                    DateOfBirth = candidate.DateOfBirth,
                    MobileNo = candidate.MobileNo,
                    IsFresher = candidate.IsFresher,
                    Picture = candidate.Picture,
                    SkillList=skillList.ToArray()
                });
            }
            return candidateSkills;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidateById(int id)
        {
            Candidate candidate = await _context.Candidates.FindAsync(id);
            Skill[] skillList = _context.CandidateSkills.Where(x => x.CandidateId == candidate.CandidateId).Select(x => new Skill { SkillId = x.SkillId }).ToArray();
            CandidateDTO candidateDTO = new CandidateDTO()
            {
                CandidateId=candidate.CandidateId,
                CandidateName=candidate.CandidateName,
                DateOfBirth=candidate.DateOfBirth,
                MobileNo=candidate.MobileNo,
                IsFresher=candidate.IsFresher,
                Picture=candidate.Picture,
                SkillList=skillList
            };
            return candidateDTO;
        }
        [HttpPost]
        public async Task<ActionResult<CandidateSkill>> PostCandidateSkill([FromForm] CandidateDTO dto)
        {
            var skillItems = JsonConvert.DeserializeObject<Skill[]>(dto.SkillStringify);
            Candidate candidate = new Candidate
            {
                CandidateName= dto.CandidateName,
                DateOfBirth=dto.DateOfBirth,
                MobileNo=dto.MobileNo,
                IsFresher = dto.IsFresher,
            };
           if(dto.PictureFile!= null)
            {
                var webRoot = _env.WebRootPath;
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.PictureFile.FileName);
                var filePath = Path.Combine(webRoot, "Images", fileName);
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await dto.PictureFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                candidate.Picture = fileName;
            }
            foreach (var item in skillItems)
            {
                var candidateskill = new CandidateSkill
                {
                    Candidate=candidate,
                    CandidateId=candidate.CandidateId,
                    SkillId=item.SkillId
                };
                _context.Add(candidateskill);
            }
            await _context.SaveChangesAsync();
            return Ok(candidate);
        }
        [Route("Update")]
        [HttpPost]
        public async Task<ActionResult<CandidateSkill>> UpdateCandidate([FromForm] CandidateDTO dto)
        {
            var skillItems = JsonConvert.DeserializeObject<Skill[]>(dto.SkillStringify);
            Candidate candidate = _context.Candidates.Find(dto.CandidateId);
            candidate.CandidateName = dto.CandidateName;
            candidate.DateOfBirth = dto.DateOfBirth;
            candidate.IsFresher = dto.IsFresher;
            candidate.MobileNo = dto.MobileNo;
            if (dto.PictureFile != null)
            {
                var webRoot = _env.WebRootPath;
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.PictureFile.FileName);
                var filePath = Path.Combine(webRoot, "Images", fileName);
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await dto.PictureFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                candidate.Picture = fileName;
            }
            var existingSkill = _context.CandidateSkills.Where(s => s.CandidateId == candidate.CandidateId).ToList();
            foreach (var skill in existingSkill)
            {
                _context.CandidateSkills.Remove(skill);
            }
            foreach (var skill in skillItems)
            {
                var candidateSkill = new CandidateSkill
                {
                    CandidateId= candidate.CandidateId,
                    SkillId= skill.SkillId
                };
                _context.Add(candidateSkill);
            }
            _context.Entry(candidate).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(candidate);
        }
        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<ActionResult<CandidateSkill>> DeletecandidateSkill(int id)
        {
            Candidate candidate = _context.Candidates.Find(id);
            var existingSkill = _context.CandidateSkills.Where(s => s.CandidateId == candidate.CandidateId).ToList();
            foreach (var skill in existingSkill)
            {
                _context.CandidateSkills.Remove(skill);
            }
            _context.Entry(candidate).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return Ok(candidate);
        }
    }
}
