using Parkinson_Models;

namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface IMemoryTestRepository : IRepository<MemoryTest>
    {
        Task<MemoryTest> UpdateTestAsync(MemoryTest memoryTest);
    }
}
