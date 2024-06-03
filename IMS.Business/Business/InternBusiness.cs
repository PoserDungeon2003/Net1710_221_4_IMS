﻿using IMS.Business.Result;
using IMS.Common;
using IMS.Data;
using IMS.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Business.Business
{
    public interface IInternBusiness
    {
        Task<BusinessResult> GetAllAsync();
        Task<BusinessResult> Add();
        Task<BusinessResult> Update();
        Task<BusinessResult> Delete();
    }
    public class InternBusiness : IInternBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public InternBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
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
            var result = await _unitOfWork.InternRepository.GetAllAsync();
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

        public Task<BusinessResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
