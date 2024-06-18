using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using static IMS.Data.Models.Intern;

namespace IMS.RazorWebApp.Pages.Intern
{
    public class IndexModel : PageModel
    {
        private readonly IInternBusiness business;
        private readonly IMS.Data.Repository.Net1710_221_4_IMSContext _context;
        public InternSearchCriteria SearchCriteria { get; set; }
        public IList<Models.Intern> Interns { get; set; }

        public IndexModel(IMS.Data.Repository.Net1710_221_4_IMSContext context)
        {
            business ??= new InternBusiness();
            _context = context;
        }

        public IList<Models.Intern> Intern { get;set; } = default!;

        public async Task OnGetAsync()
            {
            //var result = await business.Getall();
            //if(result != null && result.Status >0 && result.Data != null)
            //{
            //    Intern = result.Data as List<Models.Intern>;
            //}
            Intern = await _context.Interns
              .Include(i => i.Company)
              .Include(i => i.Mentor).ToListAsync();
            if (SearchCriteria != null && !string.IsNullOrEmpty(SearchCriteria.SearchTerm))
            {
                var searchTerm = SearchCriteria.SearchTerm.ToLower();
                Interns = Interns.Where(i =>
                    i.Name.ToLower().Contains(searchTerm) ||
                    i.University.ToLower().Contains(searchTerm) ||
                    i.Major.ToLower().Contains(searchTerm) ||
                    (i.JobPosition != null && i.JobPosition.ToLower().Contains(searchTerm)))
                    .ToList();
            }
        }
    }
}
