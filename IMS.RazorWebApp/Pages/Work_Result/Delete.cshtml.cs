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

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class DeleteModel : PageModel
    {
        private readonly WorkingResultBusiness _workingResultBusiness;

        public DeleteModel()
        {
            _workingResultBusiness = new WorkingResultBusiness();
        }

        [BindProperty]
        public WorkingResult WorkingResult { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var workingresult = await _workingResultBusiness.GetByIdAsync(id);
            if (workingresult is not null)
            {
                WorkingResult = (workingresult.Data as WorkingResult)!;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var workingresult = await _workingResultBusiness.GetByIdAsync(id);
            if (workingresult.Data is not null)
            {
                WorkingResult = (workingresult.Data as WorkingResult)!;

            }
            var result = await _workingResultBusiness.Delete(WorkingResult);
            if (result.Status != Const.SUCCESS_DELETE_CODE)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
