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
            Test = new TestRepository(_context);
            Image = new ImageRepository(_context);
        }
        public ITestRepository Test { get; private set; }
        public IImageRepository Image { get; private set; }

    }
}
