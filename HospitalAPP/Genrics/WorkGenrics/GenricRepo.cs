using HospitalAPP.Genrics.Intrefaces;
using HospitalDomain.Entites.App;
using HospitalInfrastructure.AppContext;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPP.Genrics.WorkGenrics
{
    public class GenricRepo<T> : IGenricRepo<T> where T : BaseEntity
    {
        private readonly HospitalContext _dbContext;

        public GenricRepo(HospitalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T item)
        => await _dbContext.Set<T>().AddAsync(item);

        public void DeleteAsync(T item)
        {
            _dbContext.Remove(item);
        }

        public async Task<IEnumerable<T>> GetAllWithAsync() => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T?> GetbyIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T item)
        {
            _dbContext.Update(item);
        }
    }
}