using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public int Create(TEntity entity)
        {
            _dpSet.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _dpSet.Remove(entity);
            return _context.SaveChanges();
        }

        public TEntity? Get(int id)=> _dpSet.Find(id);

        public IEnumerable<TEntity> GetAll()=> _dpSet.ToList();

        public int Update(TEntity entity)
        {
            _dpSet.Update(entity);
            return _context.SaveChanges();
        }
    }
}
