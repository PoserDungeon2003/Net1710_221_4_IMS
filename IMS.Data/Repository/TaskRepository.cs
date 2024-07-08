using IMS.Data.Base;
using Models = IMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Data.Models;
using System.Collections;

namespace IMS.Data.Repository
{
    public class TaskRepository : GenericRepository<Models.Task>
    {
        private readonly Net17102214ImsContext _context;
        public TaskRepository(Net17102214ImsContext context) => _context = context;

//        public TaskRepository()
  //      {
    //    }

        public new async Task<List<Models.Task>> GetAllAsync()
        {
            return await _context.Tasks.Include(c => c.Intern).ToListAsync();
        }

        public async Task<Models.Task> GetTaskById(int id)
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

        public async void DeleteByIdAsync()
        {
            //return await _context.Tasks.dele
        }

        public bool TaskExisted(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
        public IEnumerable GetAllTask()
        {
            return _context.Tasks;
        }
    }
}