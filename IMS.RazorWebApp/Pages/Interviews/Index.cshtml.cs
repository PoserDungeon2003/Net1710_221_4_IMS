using System;
using System.Collections.Generic;
using System.Linq;
using Tasks = System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using IMS.Data.Models;
using IMS.Common;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Drawing.Printing;

namespace IMS.RazorWebApp.Pages.Interviews
{
    public class IndexModel : PageModel
    {
        private readonly InterviewsInfoBusiness _interviewBusiness;
        [BindProperty(SupportsGet = true)]

        public string? SearchValue { get; set; }
        public int? PageSize { get; set; } = 3;

        public IndexModel(IMS.Data.Repository.Net17102214ImsContext context)
        {
            _interviewBusiness ??= new InterviewsInfoBusiness();
        }

        public PaginatedList<Models.InterviewsInfo> InterviewsInfo { get;set; } = default!;
        public async Tasks.Task OnGetAsync(int? pageIndex, int pageSize = 3)
        {
            var paginated = await _interviewBusiness.GetAllInterviewPagingAsync(pageIndex, pageSize);
            if (paginated != null)
            {
                InterviewsInfo = (PaginatedList<Models.InterviewsInfo>)paginated.Data;
            }
            if (!String.IsNullOrEmpty(SearchValue))
            {
                var searchResult = await _interviewBusiness.SearchInterview(SearchValue, pageIndex, pageSize);
                if (searchResult != null)
                {
                    InterviewsInfo = (PaginatedList<Models.InterviewsInfo>)searchResult.Data;
                }
            }
        }
    }
}
