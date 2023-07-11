using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository.IRepository;
using Parkinson_Models;

namespace Parkinson_DataAccess.Repository
{
    public class ReactionTestRepository : Repository<ReactionTest>, IReactionTestRepository
    {
        private readonly ApplicationDbContext _context;

        public ReactionTestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ReactionTest> UpdateTestAsync(ReactionTest reactionTest)
        {
            _context.ReactionTests.Update(reactionTest);
            await _context.SaveChangesAsync();
            return reactionTest;
        }
    }
}
