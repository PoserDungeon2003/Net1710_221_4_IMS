using System;
using System.Collections.Generic;
using System.Linq;
using Tasks = System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Data.DAO;
using IMS.Business.Business;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Task
{
    public class IndexModel : PageModel
    {
        private readonly ITaskBusiness _taskBusiness;
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }
        public int? PageSize { get; set; } = 3;
        public IndexModel()
        {
            _taskBusiness ??= new TaskBusiness();
        }

        //public IList<Models.Task> Task { get; set; } = default!;

        public PaginatedList<Models.Task> Task { get; set; } = default!;
        public async Tasks.Task OnGetAsync(int? pageIndex, int pageSize = 3)
        {
            var paginatedTask = await _taskBusiness.GetAllTasksPagingAsync(pageIndex, pageSize);
            if (paginatedTask != null)
            {
                Task = (PaginatedList<Models.Task>)paginatedTask.Data;
            }

            if (!String.IsNullOrEmpty(Search))
            {
                var searchResult = await _taskBusiness.SearchTasks(Search, pageIndex, pageSize);
                if (searchResult != null)
                {
                    Task = (PaginatedList<Models.Task>)searchResult.Data;
                }
            }
        }


        //{
        //    var task = await _taskBusiness.GetAllAsync();
        //    if (task != null)
        //    {
        //        Task = (IList<Models.Task>)task.Data;
        //    }

        //}



        //{
        //    var result = await _taskBusiness.GetAllAsync();
        //    if (result !=null && result.Status > 0 && result.Data != null)
        //     {
        //      Task = result.Data as List<Models.Task>;
        //     }
        //}


    }
}
