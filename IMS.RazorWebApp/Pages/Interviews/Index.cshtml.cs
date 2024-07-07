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

namespace IMS.RazorWebApp.Pages.Interviews
{
    public class IndexModel : PageModel
    {
        private readonly InterviewsInfoBusiness _interviewBusiness;
        private readonly MentorBusiness _mentorBusiness;
        private readonly InternBusiness _internBusiness;

        public IndexModel(IMS.Data.Repository.Net17102214ImsContext context)
        {
            _interviewBusiness ??= new InterviewsInfoBusiness();
            _mentorBusiness ??= new MentorBusiness();
            _internBusiness ??= new InternBusiness();
        }

        public IList<Models.InterviewsInfo> InterviewsInfo { get;set; } = default!;
        public string Message { get; set; } = string.Empty;
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var interview = await _interviewBusiness.GetAllAsync();
            if (interview.Status == Const.SUCCESS_READ_CODE)
            {
                InterviewsInfo = (interview.Data as IList<InterviewsInfo>)!;
                Message = interview.Message ?? "Get data success";
            }
            if (interview.Status == Const.WARNING_NO_DATA_CODE)
            {
                Message = interview.Message ?? "No data";
            }
            else
            {
                Message = interview.Message ?? "Read data fail";
            }
        }
    }
}
