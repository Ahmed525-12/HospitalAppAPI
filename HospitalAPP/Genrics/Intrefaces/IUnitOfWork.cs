using HospitalDomain.Entites.App;

namespace HospitalAPP.Genrics.Intrefaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CompleteAsync();

        IGenricRepo<T> Repository<T>() where T : BaseEntity;
    }
}