using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.Business.Result;
using IMS.Common;
using IMS.Data;

namespace IMS.Business.Business
{
    public interface ITaskBusiness
    {
        Task<IIMSResult> GetAllAsync();
        Task<IIMSResult> Add();
        Task<IIMSResult> Update();
        Task<IIMSResult> Delete();}
    public class TaskBusiness : ITaskBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public TaskBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
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
            var result = await _unitOfWork.TaskRepository.GetAllAsync();
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

        public Task<IIMSResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}