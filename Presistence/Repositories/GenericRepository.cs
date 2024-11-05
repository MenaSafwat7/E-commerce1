using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreContext _storeContext;
        public GenericRepository(StoreContext storeContext)
        { 
            _storeContext = storeContext;
        }
        public async Task AddAsync(TEntity entity) => await _storeContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => _storeContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _storeContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges)
            => trackChanges? await _storeContext.Set<TEntity>().ToListAsync()
            : await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();
        
        public async Task<TEntity?> GetAsync(TKey id) => await _storeContext.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity?>> GetAllAsync(Specification<TEntity> specification)
            => await ApplySpecification(specification).ToListAsync();
        

        public async Task<TEntity?> GetAsync(Specification<TEntity> specification)
            => await ApplySpecification(specification).FirstOrDefaultAsync();

        private IQueryable<TEntity> ApplySpecification(Specification<TEntity> specification)
            => SpecificationEvaluator.GetQuery<TEntity>(_storeContext.Set<TEntity>(), specification);

        public async Task<int> CoungAsync(Specification<TEntity> specification)
        => await SpecificationEvaluator.GetQuery(_storeContext.Set<TEntity>(), specification).CountAsync();
    }
}
