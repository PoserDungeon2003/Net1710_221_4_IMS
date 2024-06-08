using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Task
{
    public class CreateModel : PageModel
    {
        private readonly TaskBusiness _taskBusiness;
        private readonly InternBusiness _internBusiness;
        private readonly MentorBusiness _mentorBusiness;

        public CreateModel()
        {
            _taskBusiness ??= new TaskBusiness();
            _internBusiness ??= new InternBusiness();
            _mentorBusiness ??= new MentorBusiness();
        }

        public async Task<IActionResult> OnGet()
        {
            var intern = await _internBusiness.GetAllAsync();
            var mentor = await _mentorBusiness.GetAllAsync();
            ViewData["InternId"] = new SelectList((System.Collections.IEnumerable)intern.Data, "InternId", "Name");
            ViewData["MentorId"] = new SelectList((System.Collections.IEnumerable)mentor.Data, "MentorId", "Name");
            return Page();
        }

        [BindProperty]
        public IMS.Data.Models.Task Task { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _taskBusiness.AddAsync(Task);
            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                OnGet();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
