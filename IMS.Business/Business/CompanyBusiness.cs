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
        Task<IIMSResult> GetByIdAsync(int? id);
        Task<IIMSResult> FindAsync(int? id);
        Task<IIMSResult> AddAsync(Company company);
        Task<IIMSResult> UpdateAsync(Company company);
        Task<IIMSResult> DeleteAsync(Company company);
        IIMSResult GetAllCompany();
        IIMSResult CompanyExisted(int id);

    }
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public CompanyBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IIMSResult> AddAsync(Company company)
        {
            try
            {
                var result = await _unitOfWork.CompanyRepository.CreateAsync(company);
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

        public IIMSResult CompanyExisted(int id)
        {
            var existed = _unitOfWork.CompanyRepository.CompanyExisted(id);
            try
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, existed);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public async Task<IIMSResult> DeleteAsync(Company company)
        {
            var result = await _unitOfWork.CompanyRepository.RemoveAsync(company);

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

        public async Task<IIMSResult> FindAsync(int? id)
        {
            if (id == null)
            {
                return new BusinessResult();
            }
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync((int)id);
            try
            {
                if (company == null)
                {
                    return new BusinessResult();
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, company);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
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

        public async Task<IIMSResult> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return new BusinessResult();
            }
            var company = await _unitOfWork.CompanyRepository.GetCompanyById((int)id);
            try
            {
                if (company == null)
                {
                    return new BusinessResult();
                }
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, company);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public async Task<IIMSResult> UpdateAsync(Company company)
        {
            try
            {
                var result = await _unitOfWork.CompanyRepository.UpdateAsync(company);
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
