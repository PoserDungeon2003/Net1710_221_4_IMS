using IMS.Data.Base;
using IMS.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data.Repository
{
    public class CompanyRepository : GenericRepository<Company>
    {
        public CompanyRepository() { }

        public IEnumerable GetAllCompany()
        {
            return _context.Companies;
        }
    }
}
