using Parkinson_Models;

namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface ITestRepository : IRepository<Test>
    {
        Task<Test> UpdateTestAsync(Test test);
    }
}
