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
        private readonly Net1710_221_4_IMSContext _context;
        public WorkingResultRepository()
        {
            _context = new Net1710_221_4_IMSContext();
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