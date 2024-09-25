using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Session03.BusinessLogicLayer.repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        protected DbSet<TEntity> _dpSet;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _dpSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)=>await  _dpSet.AddAsync(entity);

        public void Delete(TEntity entity)=> _dpSet.Remove(entity);

        public async Task<TEntity?> GetAsync(int id)=>await  _dpSet.FindAsync(id);

        public async Task <IEnumerable<TEntity>> GetAllAsync()=>await  _dpSet.ToListAsync();

        public void Update(TEntity entity)=> _dpSet.Update(entity);
    }
}
