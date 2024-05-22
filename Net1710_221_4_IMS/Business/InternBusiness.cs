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
        Task<IMSResult> GetAllAsync();
        Task<IMSResult> Add();
        Task<IMSResult> Update();
        Task<IMSResult> Delete();
    }
    public class InternBusiness : IInternBusiness
    {
        private readonly InternDAO _DAO;
        public InternBusiness()
        {
            _DAO = new InternDAO();
        }

        public Task<IMSResult> Add()
        {
            throw new NotImplementedException();
        }

        public Task<IMSResult> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<IMSResult> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IMSResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
