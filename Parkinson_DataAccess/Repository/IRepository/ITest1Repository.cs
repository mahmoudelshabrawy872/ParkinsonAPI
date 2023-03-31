using Parkinson_Models;

namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface ITest1Repository : IRepository<Test1>
    {
        Task<Test1> UpdateTestAsync(Test1 test1);
    }
}
