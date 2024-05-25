using IMS.Business.Result;
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
        private readonly InternDAO _DAO;
        public InternBusiness()
        {
            _DAO = new InternDAO();
        }

        public Task<BusinessResult> Add()
        {
            throw new NotImplementedException();
        }

        public Task<BusinessResult> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<BusinessResult> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BusinessResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
