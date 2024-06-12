using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;

namespace IMS.RazorWebApp.Pages.Task
{
    public class DetailsModel : PageModel
    {
        private readonly TaskBusiness _taskBusiness;

        public DetailsModel()
        {
            _taskBusiness ??= new TaskBusiness();
        }

        public IMS.Data.Models.Task Task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskBusiness.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            else
            {
                Task = (IMS.Data.Models.Task)task.Data;
            }
            return Page();
        }
    }
}
