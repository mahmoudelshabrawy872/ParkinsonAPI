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
            Click = new ClickTestRepository(_context);
            Memory = new MemoryTestRepository(_context);
            Reaction = new ReactionTestRepository(_context);
            Spiral = new SpiralTestRepository(_context);

        }
        public ITestRepository Test { get; private set; }
        public IImageRepository Image { get; private set; }
        public IMemoryTestRepository Memory { get; private set; }
        public IClickTestRepository Click { get; private set; }
        public ISpiralTestRepository Spiral { get; private set; }
        public IReactionTestRepository Reaction { get; private set; }
    }
}
