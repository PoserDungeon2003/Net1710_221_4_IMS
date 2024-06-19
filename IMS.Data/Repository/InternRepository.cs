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
    public class InternRepository : GenericRepository<Intern>
    {
        public InternRepository() 
        {
        }
        public InternRepository(Net17102214ImsContext context) => _context = context;
        public bool InternExisted(int id)
        {
            return _context.Interns.Any(e => e.InternId == id);
        }
        public IEnumerable GetAllIntern()
        {
            return _context.Interns;
        }
    }
}
