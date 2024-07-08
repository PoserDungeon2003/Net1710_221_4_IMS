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

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class IndexModel : PageModel
    {
        private readonly IMentorBusiness _mentorBusiness;
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }
        public int? PageSize { get; set; } = 3;

        public IndexModel()
        {
            _mentorBusiness ??= new MentorBusiness();
        }

        public PaginatedList<Models.Mentor> Mentor { get;set; } = default!;

        public async Tasks.Task OnGetAsync(int? pageIndex, int pageSize = 3)
        {
            //var mentor = await _mentorBusiness.GetAllAsync();
            var paginatedMentor = await _mentorBusiness.GetAllMentorsPagingAsync(pageIndex, pageSize);
            if (paginatedMentor != null)
            {
                Mentor = (PaginatedList<Models.Mentor>)paginatedMentor.Data;
            }

            if (!String.IsNullOrEmpty(Search))
            {
                var searchResult = await _mentorBusiness.SearchMentors(Search, pageIndex, pageSize);
                if (searchResult != null)
                {
                    Mentor = (PaginatedList<Models.Mentor>)searchResult.Data;
                }
            }
        }
    }
}
