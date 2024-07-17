using IMS.Common;
using IMS.Data.Base;
using IMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data.Repository
{
    public class InternRepository : GenericRepository<Intern>
    {
        public InternRepository() 
        {
            
        }

        public InternRepository(Net17102214ImsContext context) => _context = context;

        public bool InternExisted(int id)
        {
            return _context.Interns.Any(e => e.InternId == id);
        }
        public IEnumerable GetAllIntern()
        {
            return _context.Interns;
        }

        public async Task<List<Intern>> SearchIntern(string value, int? pageIndex, int pageSize)
        {
            value = value.Trim().ToLower();
            DateOnly dateValue;
            bool isValidDate = DateOnly.TryParse(value, out dateValue);
            var mentor = _context.Interns
                                .Include(c => c.Company).Include(n => n.Mentor)
                                .OrderByDescending(m => m.InternId)
                                .Where(m =>
                                    m.University.Contains(value) ||
                                    m.Major.Contains(value) ||
                                    m.JobPosition.Contains(value) ||
                                    m.MentorId.ToString().Contains(value) ||
                                    m.EducationBackground.Contains(value) ||
                                    m.Experiences.Contains(value) ||
                                    m.WorkingTasks.Contains(value) ||
                                    m.Skills.Contains(value) ||
                                    m.Name.Contains(value) ||
                                    m.Company.Name.Contains(value));
            var paginatedMentor = await PaginatedList<Intern>.CreateAsync(mentor.AsNoTracking(), pageIndex ?? 1, pageSize);
            return paginatedMentor;
        }

        public async Task<PaginatedList<Intern>> GetInternPagingAsync(int? pageIndex, int pageSize)
        {
            var paginatedIntern = await PaginatedList<Intern>.CreateAsync(
                _context.Interns.AsNoTracking().OrderByDescending(m => m.InternId).Include(c => c.Company).Include(c => c.Mentor), pageIndex ?? 1, pageSize);
            return paginatedIntern;
        }
    }
}
