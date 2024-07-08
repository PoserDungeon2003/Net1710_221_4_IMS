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

namespace IMS.RazorWebApp.Pages.Task
{
    public class IndexModel : PageModel
    {
        private readonly TaskBusiness _taskBusiness;

        public IndexModel()
        {
            _taskBusiness ??= new TaskBusiness();
        }

        public IList<Models.Task> Task { get; set; } = default!;

        public async Tasks.Task OnGetAsync()



 //       {
 //            var task = await _taskBusiness.GetAllAsync();
 //         if (task != null)
 //         {
 //             Task = (IList<Models.Task>)task.Data;
 //           }
 //         
 //       }



        {
            var result = await _taskBusiness.GetAllAsync();
            if (result !=null && result.Status > 0 && result.Data != null)
             {
              Task = result.Data as List<Models.Task>;
             }
        }
        
      
    }
}
