using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;

namespace Parkinson_DataAccess.Repository
{
    public class ClickTestRepository : Repository<ClickTest>, IClickTestRepository
    {
        private readonly ApplicationDbContext _context;

        public ClickTestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClickTest> UpdateTestAsync(ClickTest clickTest)
        {
            _context.ClickTests.Update(clickTest);
            await _context.SaveChangesAsync();
            return clickTest;
        }
    }
}
