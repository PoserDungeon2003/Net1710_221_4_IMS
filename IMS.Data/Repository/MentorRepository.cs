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
    }
}
