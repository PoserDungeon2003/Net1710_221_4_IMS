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
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class DetailsModel : PageModel
    {
        private readonly WorkingResultBusiness _workingResultBusiness;

        public DetailsModel()
        {
            _workingResultBusiness = new WorkingResultBusiness();
        }

        public WorkingResult WorkingResult { get; set; } = default!;
        public string Message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var workingresult = await _workingResultBusiness.GetByIdAsync(id);
            Message = workingresult.Message ?? "Unknow error";
            WorkingResult = (workingresult.Data as WorkingResult)!;
            return Page();
        }
    }
}
