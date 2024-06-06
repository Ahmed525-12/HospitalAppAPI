using HospitalDomain.Entites.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPP.Genrics.Intrefaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CompleteAsync();

        IGenricRepo<T> Repository<T>() where T : BaseEntity;
    }
}