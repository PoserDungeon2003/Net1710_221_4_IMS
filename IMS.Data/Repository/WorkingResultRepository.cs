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
        public WorkingResultRepository(Net1710_221_4_IMSContext context)
        {
            _context = context;
        }
        public new async Task<IEnumerable<WorkingResult>> GetAllAsync()
        {
            return await _context.WorkingResults
            .Include(c => c.Mentor)
            .Include(c => c.Intern)
            .Include(c => c.Task)
            .ToListAsync();
        }
    }
}