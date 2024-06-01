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
using IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Intern
{
    public class DetailsModel : PageModel
    {
        private readonly InternBusiness _internBusiness;

        public DetailsModel()
        {
            _internBusiness ??= new InternBusiness();
        }

        public Models.Intern Intern { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await _internBusiness.GetByID(id);
            if (intern == null)
            {
                return NotFound();
            }
            else
            {
                Intern = (Models.Intern)intern.Data;
            }
            return Page();
        }
    }
}
