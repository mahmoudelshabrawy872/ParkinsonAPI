using Parkinson_Models;

namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface IClickTestRepository : IRepository<ClickTest>
    {
        Task<ClickTest> UpdateTestAsync(ClickTest clickTest);
    }
}
