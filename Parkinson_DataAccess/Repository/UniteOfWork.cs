using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;

namespace Parkinson_DataAccess.Repository
{
    public class UniteOfWork : IUniteOfWork
    {
        private ApplicationDbContext _context;

        public UniteOfWork(ApplicationDbContext context)
        {
            _context = context;

            Test1 = new Test1Repository(_context);

        }
        public ITest1Repository Test1 { get; private set; }
    }
}
