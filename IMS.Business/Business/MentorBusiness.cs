using IMS.Business.Result;
using IMS.Common;
using IMS.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Business.Business
{
    public interface IMentorBusiness
    {
        Task<BusinessResult> GetAllAsync();
        Task<BusinessResult> Add();
        Task<BusinessResult> Update();
        Task<BusinessResult> Delete();
    }
    public class MentorBusiness : IMentorBusiness
    {
        private MentorDAO _mentorDAO;
        public MentorBusiness()
        {
            _mentorDAO = new MentorDAO();
        }
        public Task<BusinessResult> Add()
        {
            throw new NotImplementedException();
        }

        public Task<BusinessResult> Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<BusinessResult> GetAllAsync()
        {
            var mentor = await _mentorDAO.GetAllAsync();
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, mentor);
        }

        public Task<BusinessResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
