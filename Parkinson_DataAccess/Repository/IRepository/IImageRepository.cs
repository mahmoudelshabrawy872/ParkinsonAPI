using Parkinson_Models;

namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface IImageRepository : IRepository<Image>
    {
        Task<Image> updateImageAsync(Image image);
    }
}
