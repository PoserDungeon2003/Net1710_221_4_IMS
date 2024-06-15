using IMS.Data.Base;
using IMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data.DAO
{
    public class InterviewsInfoDAO : BaseDAO<InterviewsInfo>
    {
        public InterviewsInfoDAO() { }
              public new async Task<List<InterviewsInfo>> GetAllAsync()
        {
            return await _dbSet.Include(c => c.Intern).ToListAsync();
        }
    }
}
