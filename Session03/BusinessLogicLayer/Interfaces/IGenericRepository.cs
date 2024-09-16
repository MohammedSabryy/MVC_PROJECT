namespace Session03.BusinessLogicLayer.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void Delete(TEntity entity);
        TEntity? Get(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
    }
}
