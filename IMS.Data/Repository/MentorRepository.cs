using IMS.Common;
using IMS.Data.Base;
using IMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data.Repository
{
    public class MentorRepository : GenericRepository<Mentor>
    {
        private readonly Net17102214ImsContext _context;
        public MentorRepository(Net17102214ImsContext context) => _context = context;

        public new async Task<List<Mentor>> GetAllAsync()
        {
            return await _context.Mentors.AsNoTracking().Include(c => c.Company).OrderByDescending(m => m.MentorId).ToListAsync();
        }
        
        public async Task<Mentor> GetMentorById(int id)
        {
            var mentor = await _context.Mentors.Include(c => c.Company)
                                                .FirstOrDefaultAsync(m => m.MentorId == id);
            try
            {
                if (mentor == null)
                {
                    throw new ArgumentNullException(nameof(mentor));
                }
                return mentor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<Mentor>> SearchMentor(string value, int? pageIndex, int pageSize)
        {
            value = value.Trim().ToLower();
            DateOnly dateValue;
            bool isValidDate = DateOnly.TryParse(value, out dateValue);
            var mentor = _context.Mentors
                                .Include(c => c.Company)
                                .OrderByDescending(m => m.MentorId)
                                .Where(m => 
                                    m.Email.Contains(value) || 
                                    m.Department.Contains(value) || 
                                    m.Phone.Contains(value) ||
                                    m.MentorId.ToString().Contains(value) ||
                                    m.FullName.Contains(value) ||
                                    m.Department.Contains(value) ||
                                    (isValidDate && m.DateJoined.Equals(dateValue)) ||
                                    (isValidDate && m.DateOfBirth.Equals(dateValue)) || 
                                    m.LinkedinProfile.Contains(value) || 
                                    m.Company.Name.Contains(value));
            var paginatedMentor = await PaginatedList<Mentor>.CreateAsync(mentor.AsNoTracking(), pageIndex ?? 1, pageSize);
            return paginatedMentor;
        }

        public bool MentorExisted(int id)
        {
            return _context.Mentors.Any(e => e.MentorId == id);
        }

        public IEnumerable GetAllMentor()
        {
            return _context.Mentors;
        }

        public async Task<PaginatedList<Mentor>> GetMentorsPagingAsync(int? pageIndex, int pageSize)
        {
            var paginatedMentor = await PaginatedList<Mentor>.CreateAsync(
                _context.Mentors.AsNoTracking().OrderByDescending(m => m.MentorId).Include(c => c.Company), pageIndex ?? 1, pageSize);
            return paginatedMentor;
        }
    }
}
