using IMS.Data.Base;
using IMS.Data.Models;
using IMS.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data.DAO
{
    public class MentorDAO : BaseDAO<Mentor>
    {
        public MentorDAO()
        {
        }

        public new async Task<List<Mentor>> GetAllAsync()
        {
            return await _dbSet.Include(c => c.Company).ToListAsync();
        }

    }
}
