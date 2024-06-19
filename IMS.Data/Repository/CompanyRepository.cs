using IMS.Data.Base;
using IMS.Data.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly Net17102214ImsContext _context;
        public CompanyRepository(Net17102214ImsContext context)
        {
            _context = context;
        }

        public IEnumerable GetAllCompany()
        {
            return _context.Companies;
        }

        public async Task<Company> GetCompanyById(int id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(m => m.CompanyId == id);
            try
            {
                if (company == null)
                {
                    throw new ArgumentNullException(nameof(company));
                }
                return company;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        public bool CompanyExisted(int id)
        {
            return _context.Mentors.Any(e => e.MentorId == id);
        }
    }
}
