using Microsoft.EntityFrameworkCore;
using Quoting.Database.Database;
using QuotingApi.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quoting.Database.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);  
        void Remove(T entity);

    }


    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public ChallengeDatabase Db { get; }
        public GenericRepository(ChallengeDatabase challengeDb)
        {
            Db = challengeDb;
        }


        public async void Add(T entity)
        {
            await Db.Set<T>().AddAsync(entity);
            
        }

        public async void AddRange(IEnumerable<T> entities)
        {
            await Db.Set<T>().AddRangeAsync(entities);
           
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return Db.Set<T>().Where(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Db.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await Db.Set<T>().FindAsync(id);
        }

        public  void Remove(T entity)
        {
            Db.Set<T>().Remove(entity);
            
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Db.Set<T>().RemoveRange(entities);
            
        }
    }
}
