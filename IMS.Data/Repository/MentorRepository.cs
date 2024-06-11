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
    public class MentorRepository : GenericRepository<Mentor>
    {
        private readonly Net1710_221_4_IMSContext _context;
        public MentorRepository(Net1710_221_4_IMSContext context) => _context = context;

        public new async Task<List<Mentor>> GetAllAsync()
        {
            return await _context.Mentors.Include(c => c.Company).ToListAsync();
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

        public async void DeleteByIdAsync()
        {
            //return await _context.Mentors.dele
        }

        public bool MentorExisted(int id)
        {
            return _context.Mentors.Any(e => e.MentorId == id);
        }
        public IEnumerable GetAllMentor()
        {
            return _context.Mentors;
        }
    }
}
