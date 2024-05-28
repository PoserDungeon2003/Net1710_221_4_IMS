using IMS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data
{
    public class UnitOfWork
    {
        private Net1710_221_4_IMSContext _unitOfWorkContext;
        private InternRepository _intern;
        public UnitOfWork()
        {
        }
        public InternRepository InternRepository 
        { 
            get
            { 
                return _intern ??=new Repository.InternRepository();
            } 
        }
    }
}
