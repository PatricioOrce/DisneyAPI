namespace DisneyAPI.Repositorio
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Crea un nuevo usuario en la DB
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <returns></returns>
        Task<bool> Create(T model);
        Task<List<T>> Update(T model);
        Task<bool> Delete(int id);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
    }
}
