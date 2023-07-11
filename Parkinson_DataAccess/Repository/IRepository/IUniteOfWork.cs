namespace Parkinson_DataAccess.Repository.IRepository
{
    public interface IUniteOfWork
    {
        ITestRepository Test { get; }
        IImageRepository Image { get; }
        IMemoryTestRepository Memory { get; }
        IClickTestRepository Click { get; }
        ISpiralTestRepository Spiral { get; }
        IReactionTestRepository Reaction { get; }



    }
}
