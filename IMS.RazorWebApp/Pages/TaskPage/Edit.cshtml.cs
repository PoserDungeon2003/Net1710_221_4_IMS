using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using IMS.Common;
using IMS.RazorWebApp.Pages.Intern;

namespace IMS.RazorWebApp.Pages.Task
{
    public class EditModel : PageModel
    {
        private readonly TaskBusiness _taskBusiness;
        private readonly MentorBusiness _mentorBusiness;
        private readonly InternBusiness _internBusiness;

        public EditModel()
        {
            _taskBusiness ??= new TaskBusiness();
            _mentorBusiness ??= new MentorBusiness();
            _internBusiness ??= new InternBusiness();
        }

        [BindProperty]
        public IMS.Data.Models.Task Task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) //(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Load combobox
            //ViewData["CustomerId"] = new SelectList(_context.Customer, "", "")

            var task = await _taskBusiness.GetByIdAsync(id);
            var mentor = await _mentorBusiness.GetAllAsync();
            var intern = await _internBusiness.Getall();
            if (task == null)
            {
                return NotFound();
            }
            Task = (IMS.Data.Models.Task)task.Data;
            ViewData["InternId"] = new SelectList((System.Collections.IEnumerable)intern.Data, "InternId", "Name");
            ViewData["MentorId"] = new SelectList((System.Collections.IEnumerable)mentor.Data, "MentorId", "Name");

            //Currency = currency.Data as Currency

            return Page();

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _taskBusiness.UpdateAsync(Task);
                if (result.Status != Const.SUCCESS_UPDATE_CODE)
                {
                    await OnGetAsync(Task.TaskId);
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(bool)_taskBusiness.TaskExisted(Task.TaskId).Data)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
