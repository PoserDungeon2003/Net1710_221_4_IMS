using IMS.Business.Result;
using IMS.Common;
using IMS.Data;
using IMS.Data.DAO;
using IMS.Data.Models;
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
        Task<IIMSResult> AddAsync(Mentor mentor);
        Task<IIMSResult> Update();
        Task<IIMSResult> Delete();
    }
    public class MentorBusiness : IMentorBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public MentorBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IIMSResult> AddAsync(Mentor mentor)
        {
            try
            {
                var result = await _unitOfWork.MentorRepository.CreateAsync(mentor);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public Task<IIMSResult> Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<IIMSResult> GetAllAsync()
        {
            var mentor = await _unitOfWork.MentorRepository.GetAllAsync();
            try
            {
                if (mentor == null)
                {
                    return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, null);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, mentor);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public Task<IIMSResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
