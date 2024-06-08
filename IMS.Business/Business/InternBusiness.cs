using IMS.Common;
using IMS.Data.DAO;
using IMS.Data.Models;
using IMS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using IMS.Business.Result;
using IMS.Data;

namespace IMS.Business.Business
{
    public interface IInternBusiness
    {
        Task<IIMSResult> Getall();
        Task<IIMSResult> GetByID(int? id);
        Task<IIMSResult> Save(Intern intern);
        Task<IIMSResult> Update(Intern intern);
        Task<IIMSResult> DeleteById(string code);
        Task<IIMSResult> DeleteAsync(Intern intern);
    }
    public class InternBusiness : IInternBusiness
    {
        //private readonly InternDAO _DAO;
        private readonly UnitOfWork _unitOfWork;
        public InternBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IIMSResult> Getall()
        {
            try
            {
                #region Business rule
                #endregion

                //var interns = await _DAO.GetAllAsync();
                var intern = await _unitOfWork.InternRepository.GetAllAsync();

                if (intern == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_CREATE_MSG, intern);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IIMSResult> GetByID(int? id)
        {
            try
            {
                #region Business rule
                #endregion

                //var currency = await _DAO.GetByIdAsync(code);
                var intern = await _unitOfWork.InternRepository.GetByIdAsync((int)id);
                if (intern == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, intern);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IIMSResult> Save(Intern intern)
        {
            try
            {
                //
                //int result = await _DAO.CreateAsync(intern);
                _unitOfWork.InternRepository.PrepareCreate(intern);
                int result = await _unitOfWork.InternRepository.SaveAsync();
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IIMSResult> Update(Intern intern)
        {
            try
            {
                //int result = await _DAO.UpdateAsync(intern);
                int result = await _unitOfWork.InternRepository.UpdateAsync(intern);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IIMSResult> DeleteById(string code)
        {
            try
            {
                //var currency = await _DAO.GetByIdAsync(code);
                var interns = await _unitOfWork.InternRepository.GetByIdAsync(code);
                if (interns != null)
                {
                    //var result = await _DAO.RemoveAsync(currency);
                    var result = await _unitOfWork.InternRepository.RemoveAsync(interns);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public IIMSResult InternExisted(int id)
        {
            var existed = _unitOfWork.InternRepository.InternExisted(id);
            try
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, existed);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public async Task<IIMSResult> DeleteAsync(Intern intern)
        {
            var result = await _unitOfWork.InternRepository.RemoveAsync(intern);

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
    }
}

