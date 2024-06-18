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
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Intern
{
    public class DeleteModel : PageModel
    {
        private readonly InternBusiness business;

        public DeleteModel()
        {
            business = new InternBusiness();
        }

        [BindProperty]
        public Models.Intern Intern { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await business.GetByID(id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await business.GetByID(id);
            if (intern != null)
            {
                Intern = (Models.Intern)intern.Data;
                var result = await business.DeleteAsync(Intern);

                if (result.Status != Const.SUCCESS_DELETE_CODE)
                {
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
