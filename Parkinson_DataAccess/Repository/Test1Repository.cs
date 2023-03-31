using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;

namespace Parkinson_DataAccess.Repository
{
    public class Test1Repository : Repository<Test1>, ITest1Repository
    {
        private ApplicationDbContext _context;
        public Test1Repository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Test1> UpdateTestAsync(Test1 test1)
        {
            _context.Test1s.Update(test1);
            await _context.SaveChangesAsync();
            return test1;
        }
    }
}
