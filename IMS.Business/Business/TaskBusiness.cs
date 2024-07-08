using IMS.Business.Result;
using IMS.Common;
using IMS.Data;
using IMS.Data.DAO;
using IMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IMS.Business.Business
{
    public interface ITaskBusiness
    {
        Task<IIMSResult> GetAllAsync();
        Task<IIMSResult> FindAsync(int? id);
        Task<IIMSResult> AddAsync(IMS.Data.Models.Task task);
        Task<IIMSResult> GetByIdAsync(int? id);
        Task<IIMSResult> UpdateAsync(IMS.Data.Models.Task task);
        Task<IIMSResult> DeleteAsync(IMS.Data.Models.Task task);
        IIMSResult TaskExisted(int id);
        Task<IIMSResult> DeleteByIdAsync(int? id);
    }
    public class TaskBusiness : ITaskBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public TaskBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IIMSResult> AddAsync(IMS.Data.Models.Task task)
        {
            try
            {
                var result = await _unitOfWork.TaskRepository.CreateAsync(task);
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

        public async Task<IIMSResult> DeleteAsync(IMS.Data.Models.Task task)
        {
            var result = await _unitOfWork.TaskRepository.RemoveAsync(task);

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
            var task = await _unitOfWork.TaskRepository.GetAllAsync();
            try
            {
                if (task == null)
                {
                    return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, null);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, task);
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
            var task = await _unitOfWork.TaskRepository.GetByIdAsync((int)id);
            try
            {
                if (task == null)
                {
                    return new BusinessResult();
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, task);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public async Task<IIMSResult> UpdateAsync(IMS.Data.Models.Task task)
        {
            try
            {
                var result = await _unitOfWork.TaskRepository.UpdateAsync(task);
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
            var task = await _unitOfWork.TaskRepository.GetTaskById((int)id);
            try
            {
                if (task == null)
                {
                    return new BusinessResult();
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, task);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public IIMSResult TaskExisted(int id)
        {
            var existed = _unitOfWork.TaskRepository.TaskExisted(id);
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
                var task = await _unitOfWork.TaskRepository.GetByIdAsync((int)id);
                if (task != null)
                {
                    var result = await _unitOfWork.TaskRepository.RemoveAsync(task);
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
