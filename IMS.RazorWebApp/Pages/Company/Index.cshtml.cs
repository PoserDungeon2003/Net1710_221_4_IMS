using System;
using System.Collections.Generic;
using System.Linq;
using Tasks = System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;

namespace IMS.RazorWebApp.Pages.Company
{
    public class IndexModel : PageModel
    {
        private readonly ICompanyBusiness _companyBusiness;

        public IndexModel()
        {
            _companyBusiness = new CompanyBusiness();
        }

        public IList<Models.Company> Company { get;set; } = default!;

        public async Tasks.Task OnGetAsync()
        {
            var company = await _companyBusiness.GetAllAsync();
            if (company != null)
            {
                Company = (IList<Models.Company>) company.Data;
            }
        }
    }
}
