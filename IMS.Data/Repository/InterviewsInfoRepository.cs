using IMS.Data.Base;
using IMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Data.Repository
{
    public class InterviewsInfoRepository : GenericRepository<InterviewsInfo>
    {
        public InterviewsInfoRepository() { }

        public new async Task<List<InterviewsInfo>> GetAllAsync()
        {
            return await _context.InterviewsInfos.Include(c => c.Intern).ToListAsync();
        }

        public async Task<InterviewsInfo> GetInterviewInfoById(int id)
        {
            var interview = await _context.InterviewsInfos.FirstOrDefaultAsync(i => i.InterviewinfoId == id);
            try
            {
                if (interview == null)
                {
                    throw new ArgumentNullException(nameof(interview));
                }
                return interview;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        public bool InterviewExisted(int id)
        {
            return _context.InterviewsInfos.Any(e => e.InterviewinfoId == id);
        }
    }
}
