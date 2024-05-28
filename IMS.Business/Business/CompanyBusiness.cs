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
    public interface ICompanyBusiness
    {
        Task<IIMSResult> GetAllAsync();
        IIMSResult GetAllCompany();
        Task<IIMSResult> GetByIdAsync(int? id);
        Task<IIMSResult> AddAsync();
        Task<IIMSResult> Update();
        Task<IIMSResult> Delete();
    }
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public CompanyBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public Task<IIMSResult> AddAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IIMSResult> Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<IIMSResult> GetAllAsync()
        {
            var company = await _unitOfWork.CompanyRepository.GetAllAsync();
            try
            {
                if (company == null)
                {
                    return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, null);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, company);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public IIMSResult GetAllCompany()
        {
            var company = _unitOfWork.CompanyRepository.GetAllCompany();
            try
            {
                if (company == null)
                {
                    return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, null);
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, company);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public Task<IIMSResult> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IIMSResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
