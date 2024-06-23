using IMS.Data.Base;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data.DAO
{
    public class TaskDAO : BaseDAO<Models.Task>
    {
        public TaskDAO()
        {
        }

        public new async Task<List<Models.Task>> GetAllAsync()
        {
            return await _dbSet.Include(c => c.Intern).ToListAsync();
        }

    }
}
