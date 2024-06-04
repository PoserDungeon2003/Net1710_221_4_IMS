﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;
using NuGet.Protocol;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using IMS.Business.Business;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class IndexModel : PageModel
    {
        private readonly WorkingResultBusiness _workingResultBusiness;

        public IndexModel()
        {
            _workingResultBusiness = new WorkingResultBusiness();
        }

        public IList<WorkingResult> WorkingResult { get; set; } = default!;
        public string Message { get; set; } = string.Empty;

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var workingResult = await _workingResultBusiness.GetAllAsync();
            Message = workingResult.Message ?? "Unknow error";
            WorkingResult = (workingResult.Data as IList<WorkingResult>)!;
        }
    }
}
