using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.Business.Result;
using IMS.Common;
using IMS.Data;
using IMS.Data.Models;
using IMS.Data.Repository;
using Microsoft.Extensions.Primitives;

namespace IMS.Business.Business
{
    public interface IWorkingResultBusiness
    {
        Task<IIMSResult> GetAllAsync();
        Task<IIMSResult> GetByIdAsync(int? id);
        Task<IIMSResult> AddAsync(WorkingResult workingResult);
        Task<IIMSResult> Update(WorkingResult workingResult);
        Task<IIMSResult> Delete(WorkingResult workingResult);
    }
    public class WorkingResultBusiness : IWorkingResultBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public WorkingResultBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IIMSResult> AddAsync(WorkingResult workingResult)
        {
            var result = await _unitOfWork.WorkingResultRepository.CreateAsync(workingResult);
            try
            {
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

        public async Task<IIMSResult> Delete(WorkingResult workingResult)
        {
            var result = await _unitOfWork.WorkingResultRepository.RemoveAsync(workingResult);
            try
            {
                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_DELETE_CODE, ex.ToString());
            }

        }

        public async Task<IIMSResult> GetAllAsync()
        {
            var result = await _unitOfWork.WorkingResultRepository.GetAllAsync();
            try
            {
                if (result.Count() == 0)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public async Task<IIMSResult> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
            }
            var result = await _unitOfWork.WorkingResultRepository.GetByIdAsync((int)id);
            try
            {
                if (result == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public async Task<IIMSResult> Update(WorkingResult workingResult)
        {
            var result = await _unitOfWork.WorkingResultRepository.UpdateAsync(workingResult);
            try
            {
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
    }
}