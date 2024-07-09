using IMS.Common;
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
        private readonly Net17102214ImsContext _context;
        public InterviewsInfoRepository(Net17102214ImsContext context) => _context = context;

        public new async Task<List<InterviewsInfo>> GetAllAsync()
        {
            return await _context.InterviewsInfos.Include(c => c.Intern).Include(c => c.Mentor).ToListAsync();
        }

        public async Task<InterviewsInfo> GetInterviewInfoById(int id)
        {
            var interview = await _context.InterviewsInfos.Include(c => c.Intern).Include(c => c.Mentor)
                                                        .FirstOrDefaultAsync(i => i.InterviewinfoId == id);
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

        public async Task<List<InterviewsInfo>> SearchInterview(string value, int? pageIndex, int pageSize)
        {
            value = value.Trim().ToLower();
            DateOnly dateValue;
            bool isValidDate = DateOnly.TryParse(value, out dateValue);
            var interview = _context.InterviewsInfos
                                .Include(c => c.Intern)
                                .Include(c => c.Mentor)
                                .Where(m =>
                                    m.Mentor.FullName.Contains(value) ||
                                    m.Intern.Name.Contains(value) ||
                                    m.InterviewMode.Contains(value) ||
                                    m.Content.Contains(value) ||
                                    m.Location.Contains(value) ||
                                    (isValidDate && m.Time.Date.Equals(dateValue)) ||
                                    m.InterviewStatus.Equals(value));
            var paginated = await PaginatedList<InterviewsInfo>.CreateAsync(interview.AsNoTracking(), pageIndex ?? 1, pageSize);
            return paginated;
        }
        public async Task<PaginatedList<InterviewsInfo>> GetInterviewPagingAsync(int? pageIndex, int pageSize)
        {
            var paginated = await PaginatedList<InterviewsInfo>.CreateAsync(
                _context.InterviewsInfos.AsNoTracking().Include(c => c.Intern).Include(c => c.Mentor), pageIndex ?? 1, pageSize);
            return paginated;
        }
    }
}
