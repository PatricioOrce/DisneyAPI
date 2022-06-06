using DisneyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DisneyAPI.Repositorio
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly DisneyContext _ctx;
        public Repository(DisneyContext ctx)
        {
            this._ctx = ctx;    
        }
        /// <summary>
        /// Crea un nuevo usuario en la DB
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <returns></returns>
        public async Task<bool> Create(T model)
        {
            try{
                await _ctx.Set<T>().AddAsync(model);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
       
        public async Task<bool> Delete(int id)
        {
            T model;
            try{
                model = await _ctx.Set<T>().FindAsync(id);
                _ctx.Set<T>().Remove(model);
                if (model is not null)
                {
                    await _ctx.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await _ctx.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetById(int id)
        {
            T model = await _ctx.Set<T>().FindAsync(id);
            return model;
        }
        /// <summary>
        /// Actualiza la entidad enviada por parametro y devuelve una lista actualizada, de lo contrario devuelve una lista vacia.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<T>> Update(T model)
        {
            List<T> listModel = new();
            _ctx.Set<T>().Update(model);
            try
            {
                listModel = await GetAll();
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listModel;
        }

      
    }
}
