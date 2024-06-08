using IMS.Data.Base;
using Models = IMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data.Repository
{
    public class TaskRepository : GenericRepository<IMS.Data.Models.Task>
    {
        public TaskRepository() { }

        public new async Task<List<Models.Task>> GetAllAsync()
        {
            return await _dbSet.Include(c => c.Intern).ToListAsync();
        }

        public async Task<IMS.Data.Models.Task> GetTaskById(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(m => m.TaskId == id);
            try
            {
                if (task == null)
                {
                    throw new ArgumentNullException(nameof(task));
                }
                return task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        public bool TaskExisted(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
    }

    ////TO DO CODE HERE////


}
