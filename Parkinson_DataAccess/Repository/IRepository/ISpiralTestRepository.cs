using Parkinson_Models;

namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface ISpiralTestRepository : IRepository<SpiralTest>
    {
        Task<SpiralTest> UpdateTestAsync(SpiralTest spiralTest);
    }
}
