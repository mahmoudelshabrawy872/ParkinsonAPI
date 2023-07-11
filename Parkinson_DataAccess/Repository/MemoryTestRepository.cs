using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;

namespace Parkinson_DataAccess.Repository
{
    public class MemoryTestRepository : Repository<MemoryTest>, IMemoryTestRepository
    {
        private readonly ApplicationDbContext _context;

        public MemoryTestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MemoryTest> UpdateTestAsync(MemoryTest memoryTest)
        {
            _context.MemoryTests.Update(memoryTest);
            await _context.SaveChangesAsync();
            return memoryTest;
        }
    }
}
