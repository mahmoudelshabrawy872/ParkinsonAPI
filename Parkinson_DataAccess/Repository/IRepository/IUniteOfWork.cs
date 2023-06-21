namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface IUniteOfWork
    {
        ITestRepository Test { get; }
        IImageRepository Image { get; }

    }
}
