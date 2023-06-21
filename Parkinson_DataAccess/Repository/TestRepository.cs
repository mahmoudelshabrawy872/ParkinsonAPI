using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;

namespace Parkinson_DataAccess.Repository
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        private ApplicationDbContext _context;
        public TestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Test> UpdateTestAsync(Test test)
        {
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();
            return test;
        }
    }
}
