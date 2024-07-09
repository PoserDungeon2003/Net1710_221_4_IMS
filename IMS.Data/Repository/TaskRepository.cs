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
using IMS.Common;

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


        public async Task<List<Models.Task>> SearchTask(string value, int? pageIndex, int pageSize)
        {
            value = value.Trim().ToLower();
            DateOnly dateValue;
            bool isValidDate = DateOnly.TryParse(value, out dateValue);
            var task = _context.Tasks
                                .Include(c => c.WorkingResults)
                                .OrderByDescending(m => m.TaskId)
                                .Where(m =>
                                    m.TaskId.ToString().Contains(value) ||
                                    m.Name.ToLower().Contains(value) ||
                                    m.Description.ToLower().Contains(value) ||
                                    m.Priority.ToString().Contains(value) ||
                                    m.Status.ToLower().Contains(value) ||
                                   (isValidDate && m.CreateDate.Equals(dateValue)) ||
                                   (isValidDate && m.DueDate.Equals(dateValue)) ||
                                    m.CompletionPercentage.ToString().Contains(value) ||
                                    m.InternId.ToString().Contains(value) ||
                                    m.MentorId.ToString().Contains(value));

            var paginatedTask = await PaginatedList<Models.Task>.CreateAsync(task.AsNoTracking(), pageIndex ?? 1, pageSize);
            return paginatedTask;
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

        public async Task<PaginatedList<Models.Task>> GetTasksPagingAsync(int? pageIndex, int pageSize)
        {
            var paginatedTask = await PaginatedList<Models.Task>.CreateAsync(
                _context.Tasks.AsNoTracking().OrderByDescending(m => m.TaskId).Include(c => c.WorkingResults), pageIndex ?? 1, pageSize);
            return paginatedTask;
        }
    }
}