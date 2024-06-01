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
        Task<IIMSResult> FindAsync(int? id);
        Task<IIMSResult> AddAsync(Mentor mentor);
        Task<IIMSResult> GetByIdAsync(int? id);
        Task<IIMSResult> UpdateAsync(Mentor mentor);
        Task<IIMSResult> DeleteAsync(Mentor mentor);
        IIMSResult MentorExisted(int id);
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
                return new BusinessResult(Const.FAIL_CREATE_CODE, ex.ToString());
            }
        }

        public async Task<IIMSResult> DeleteAsync(Mentor mentor)
        {
            var result = await _unitOfWork.MentorRepository.RemoveAsync(mentor);
            
            try
            {
                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }
                return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_DELETE_CODE, ex.ToString());
            }
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
                public IIMSResult GetAllMentor()
        {
            var mentor = _unitOfWork.MentorRepository.GetAllMentor();
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
        public async Task<IIMSResult> FindAsync(int? id)
        {
            if (id == null)
            {
                return new BusinessResult();
            }
            var mentor = await _unitOfWork.MentorRepository.GetByIdAsync((int)id);
            try
            {
                if (mentor == null)
                {
                    return new BusinessResult();
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, mentor);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public async Task<IIMSResult> UpdateAsync(Mentor mentor)
        {
            try
            {
                var result = await _unitOfWork.MentorRepository.UpdateAsync(mentor);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, ex.ToString());
            }
        }

        public async Task<IIMSResult> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return new BusinessResult();
            }
            var mentor = await _unitOfWork.MentorRepository.GetMentorById((int)id);
            try
            {
                if (mentor == null)
                {
                    return new BusinessResult();
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, mentor);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public IIMSResult MentorExisted(int id)
        {
            var existed = _unitOfWork.MentorRepository.MentorExisted(id);
            try
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, existed);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public async System.Threading.Tasks.Task AddAsync(Intern intern)
        {
            throw new NotImplementedException();
        }
    }
}
