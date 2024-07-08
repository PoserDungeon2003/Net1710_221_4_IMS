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
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Task
{
    public class DeleteModel : PageModel
    {
        private readonly TaskBusiness _taskBusiness;

        public DeleteModel()
        {
            _taskBusiness ??= new TaskBusiness();
        }

        [BindProperty]
        public IMS.Data.Models.Task Task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskBusiness.FindAsync(id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskBusiness.FindAsync(id);
            if (task != null)
            {
                Task = (IMS.Data.Models.Task)task.Data;
                var result = await _taskBusiness.DeleteAsync(Task);

                if (result.Status != Const.SUCCESS_DELETE_CODE)
                {
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}

