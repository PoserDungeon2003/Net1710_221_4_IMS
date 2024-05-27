using IMS.Data.Base;
using IMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data.Repository
{
    public class MentorRepository : GenericRepository<Mentor>
    {
        public MentorRepository() { }

        public new async Task<List<Mentor>> GetAllAsync()
        {
            return await _dbSet.Include(c => c.Company).ToListAsync();
        }
        
        public async Task<Mentor> GetMentorById(int id)
        {
            var mentor = await _context.Mentors.FirstOrDefaultAsync(m => m.MentorId == id);
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

        public bool MentorExisted(int id)
        {
            return _context.Mentors.Any(e => e.MentorId == id);
        }
    }
}
