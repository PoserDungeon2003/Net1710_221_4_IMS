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
        Task<IIMSResult> GetAllAsync();
        Task<IIMSResult> Add();
        Task<IIMSResult> Update();
        Task<IIMSResult> Delete();
    }
    public class MentorBusiness : IMentorBusiness
    {
        private MentorDAO _mentorDAO;
        public MentorBusiness()
        {
            _mentorDAO = new MentorDAO();
        }
        public Task<IIMSResult> Add()
        {
            throw new NotImplementedException();
        }

        public Task<IIMSResult> Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<IIMSResult> GetAllAsync()
        {
            var mentor = await _mentorDAO.GetAllAsync();
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, mentor);
        }

        public Task<IIMSResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
