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

        private MentorRepository _mentor;

        public UnitOfWork() { }

        public MentorRepository MentorRepository 
        { 
            get
            {
                return _mentor ?? new MentorRepository();
            }
        }
    }
}
