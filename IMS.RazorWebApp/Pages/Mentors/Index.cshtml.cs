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

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class IndexModel : PageModel
    {
        private readonly IMentorBusiness _mentorBusiness;

        public IndexModel()
        {
            _mentorBusiness ??= new MentorBusiness();
        }

        public IList<Models.Mentor> Mentor { get;set; } = default!;

        public async Tasks.Task OnGetAsync()
        {
            var mentor = await _mentorBusiness.GetAllAsync();
            if (mentor != null)
            {
                Mentor = (IList<Models.Mentor>)mentor.Data;
            }
        }
    }
}
