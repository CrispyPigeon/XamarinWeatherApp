using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model.DbItems.Base;
using SQLite;

namespace DBL.Service
{
    public class DatabaseService<T, TId> : IDatabaseService<T>
        where T : IDbModel<TId>, new()
    {
        public SQLiteAsyncConnection Connection { get; }

        public DatabaseService(IDbAsyncContext context)
        {
            Connection = context.Connection;
        }


        public async Task Add(T data)
        {
            try
            {
                await Connection.InsertAsync(data)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task AddRange(IEnumerable<T> data)
        {
            try
            {
                await Connection.InsertAllAsync(data)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<T> GetById(object id)
        {
            try
            {
                return await Connection.GetAsync<T>(id)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<bool> Exists(object id)
        {
            return await Connection.FindAsync<T>(id)
                .ConfigureAwait(false) != null;
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await Connection.Table<T>()
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<List<T>> Where(Expression<Func<T, bool>> predicateExpression)
        {
            try
            {
                return await Connection.Table<T>()
                    .Where(predicateExpression)
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicateExpression)
        {
            try
            {
                return await Connection.Table<T>()
                    .FirstOrDefaultAsync(predicateExpression)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<T> FirstOrDefault()
        {
            try
            {
                return await Connection.Table<T>()
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task Delete(T data)
        {
            try
            {
                await Connection.DeleteAsync(data)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task DeleteAll()
        {
            try
            {
                await Connection.DeleteAllAsync<T>()
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task DeleteAll(IEnumerable<T> data)
        {
            try
            {
                foreach (T item in data)
                {
                    await Connection.DeleteAsync(item)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task Update(T data)
        {
            try
            {
                await Connection.UpdateAsync(data)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task UpdateAll(IEnumerable<T> data)
        {
            try
            {
                await Connection.UpdateAllAsync(data)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task AddOrUpdate(T data)
        {
            if (await Exists(data.Id))
            {
                await Update(data)
                    .ConfigureAwait(false);
            }
            else
            {
                await Add(data)
                    .ConfigureAwait(false);
            }
        }

        public async Task AddOrUpdateAll(IEnumerable<T> rangeData)
        {
            foreach (T data in rangeData)
            {
                await AddOrUpdate(data)
                    .ConfigureAwait(false);
            }
        }

        public async Task DropTable()
        {
            try
            {
                await Connection.DropTableAsync<T>()
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
