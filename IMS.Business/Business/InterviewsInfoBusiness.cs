using IMS.Business.Result;
using IMS.Common;
using IMS.Data;
using IMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Business.Business
{
    public interface IinterviewsInfoBusiness
    {
        Task<IIMSResult> GetAllAsync();
        Task<IIMSResult> FindAsync(int? id);
        Task<IIMSResult> AddAsync(InterviewsInfo interviewsInfo);
        Task<IIMSResult> GetByIdAsync(int? id);
        Task<IIMSResult> UpdateAsync(InterviewsInfo interviewsInfo);
        Task<IIMSResult> DeleteAsync(InterviewsInfo interviewsInfo);
        Task<IIMSResult> DeleteByIdAsync(int? id);
        IIMSResult InterviewInfoExisted(int id);
    }
    public class InterviewsInfoBusiness : IinterviewsInfoBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public InterviewsInfoBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IIMSResult> AddAsync(InterviewsInfo interviewsInfo)
        {
            try
            {
                var result = await _unitOfWork.InterviewsInfoRepository.CreateAsync(interviewsInfo);
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

        public async Task<IIMSResult> DeleteAsync(InterviewsInfo interviewInfo)
        {
            var result = await _unitOfWork.InterviewsInfoRepository.RemoveAsync(interviewInfo);

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
            var mentor = await _unitOfWork.InterviewsInfoRepository.GetAllAsync();
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
            var mentor = await _unitOfWork.InterviewsInfoRepository.GetByIdAsync((int)id);
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

        public async Task<IIMSResult> UpdateAsync(InterviewsInfo interviewsInfo)
        {
            try
            {
                var result = await _unitOfWork.InterviewsInfoRepository.UpdateAsync(interviewsInfo);
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
            var interviewsInfo = await _unitOfWork.InterviewsInfoRepository.GetInterviewInfoById((int)id);
            try
            {
                if (interviewsInfo == null)
                {
                    return new BusinessResult();
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, interviewsInfo);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public IIMSResult InterviewInfoExisted(int id)
        {
            var existed = _unitOfWork.InterviewsInfoRepository.InterviewExisted(id);
            try
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, existed);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }
        public async Task<IIMSResult> DeleteByIdAsync(int? id)
        {
            try
            {
                var interview = await _unitOfWork.InterviewsInfoRepository.GetByIdAsync((int)id);
                if (interview != null)
                {
                    var result = await _unitOfWork.InterviewsInfoRepository.RemoveAsync(interview);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_DELETE_CODE, ex.ToString());
            }
        }
    }
}
