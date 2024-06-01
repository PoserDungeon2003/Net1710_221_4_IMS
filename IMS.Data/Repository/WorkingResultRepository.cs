using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.Data.Base;
using IMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IMS.Data.Repository
{
    public class WorkingResultRepository : GenericRepository<WorkingResult>
    {
        public WorkingResultRepository()
        {
        }
        public new async Task<IEnumerable<WorkingResult>> GetAllAsync()
        {
            return await _dbSet
            .Include(c => c.Mentor)
            .Include(c => c.Intern)
            .Include(c => c.Task)
            .ToListAsync();
        }
    }
}