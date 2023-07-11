using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;

namespace Parkinson_DataAccess.Repository
{
    public class SpiralTestRepository : Repository<SpiralTest>, ISpiralTestRepository
    {
        private readonly ApplicationDbContext _context;

        public SpiralTestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SpiralTest> UpdateTestAsync(SpiralTest spiralTest)
        {
            _context.SpiralTests.Update(spiralTest);
            await _context.SaveChangesAsync();
            return spiralTest;
        }
    }
}
