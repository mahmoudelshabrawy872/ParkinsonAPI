using Parkinson_Models;

namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface IReactionTestRepository : IRepository<ReactionTest>
    {
        Task<ReactionTest> UpdateTestAsync(ReactionTest reactionTest);
    }
}
