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

namespace IMS.Business
{ public interface IInternBusiness
    { 
        Task<IIMSResult> Getall();
        Task<IIMSResult> GetByID(string code);
        Task<IIMSResult> Save(Intern intern);
        Task<IIMSResult> Update(Intern intern);
        Task<IIMSResult> DeleteById(string code);
    }
    public class InternBusiness : IInternBusiness
    {
        private readonly InternDAO _DAO;
        public InternBusiness()
        {
            _DAO = new InternDAO();
        }
        public async Task<IIMSResult> Getall()
        {
            try
            {
                #region Business rule
                #endregion

                var interns = await _DAO.GetAllAsync();

                if (interns == null)
                {
                    return new IMSResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new IMSResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_CREATE_MSG, interns);
                }
            }
            catch (Exception ex)
            {
                return new IMSResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IIMSResult> GetByID(string code)
        {
            try
            {
                #region Business rule
                #endregion

                var currency = await _DAO.GetByIdAsync(code);

                if (currency == null)
                {
                    return new IMSResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new IMSResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currency);
                }
            }
            catch (Exception ex)
            {
                return new IMSResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IIMSResult> Save(Intern intern)
        {
            try
            {
                //

                int result = await _DAO.CreateAsync(intern);
                if (result > 0)
                {
                    return new IMSResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new IMSResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new IMSResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IIMSResult> Update(Intern intern)
        {
            try
            {
                int result = await _DAO.UpdateAsync(intern);
                if (result > 0)
                {
                    return new IMSResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new IMSResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new IMSResult(-4, ex.ToString());
            }
        }

        public async Task<IIMSResult> DeleteById(string code)
        {
            try
            {
                var currency = await _DAO.GetByIdAsync(code);
                if (currency != null)
                {
                    var result = await _DAO.RemoveAsync(currency);
                    if (result)
                    {
                        return new IMSResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new IMSResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new IMSResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new IMSResult(-4, ex.ToString());
            }
        }
    }
}

